using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NewAIScript : MonoBehaviour
{
    Animator myAnimator; 
    NavMeshAgent myAgent;

    public LayerMask whatIsGround, whatIsPlayer;
    Transform player;

    public Transform firePosition;

    //Attacking Behavior
    public float attackRange, attackTime;
    private bool playerInAttackRange, readyToAttack = true;
    public GameObject attackProjectile;

    //Guarding Behavior
    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    //Chasing Behavior
    public float chaseRange;
    private bool playerInChaseRange;

    //Melee
    public bool meleeAttacker;
    public int meleeDamageAmount = 2;

    //Dodging Behavior    This is for adding in my random dodge mechanic
    public float dodgeRange;
    public float dodgeSpeed;
    private bool playerInDodgeRange;
    float time;
    bool startDodgeDelay = false;
    float dodgeDelay;
    public float minDodgeTime;
    public float maxDodgeTime;
    Rigidbody rb;

    //Refs
    LevelManager LMRef;  //references the levelManager


    void Start()
    {
        LMRef = FindObjectOfType<LevelManager>(); //sets LMRef = to the levelManager in the scene
        meleeDamageAmount += LMRef.damageGain; //applies the damageGain to the damage of the meleeDamageAmount

        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();

        time = 1;
        dodgeDelay = Random.Range(minDodgeTime, maxDodgeTime); //This randomizes the time between dodges

        rb = gameObject.GetComponent<Rigidbody>(); //using rigidbody force for dodging
    }

    void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInDodgeRange = Physics.CheckSphere(transform.position, dodgeRange, whatIsPlayer);   //this is for the enemy dodging back and forth when they get closer to the player. The enemy can also shoot while dodging. Dodging wil NOT be applied to the knifers


        if (!playerInChaseRange && !playerInAttackRange && !playerInDodgeRange)
        {
            Guarding();
            ResetDodging();
        }

        if (playerInChaseRange && !playerInAttackRange)
        {
            ChasingPlayer();
            ResetDodging();
        }

        if (playerInChaseRange && playerInAttackRange && !playerInDodgeRange)
        {
            AttackingPlayer();
            ResetDodging();
        }

        if (playerInChaseRange && playerInDodgeRange && playerInAttackRange && !meleeAttacker)
        {
            DodgingAndAttackingPlayer();               //calls on the dodging function i have created
            startDodgeDelay = true;                  //Starts the dodge delay
        }
    }

    private void Guarding()
    {
        if (!destinationSet)
        {
            SearchForDestination();
        }
        else
        {
            myAgent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestination = transform.position - destinationPoint;

        if (distanceToDestination.magnitude < 1f)
        {
            destinationSet = false;
        }
    }

    private void ChasingPlayer()
    {
        myAgent.SetDestination(player.position);
    }

    private void AttackingPlayer()
    {
        myAgent.SetDestination(player.position);    //Still chasing the player here instead of just stopping
        FaceTarget(); //Instead of using transform.lookat(player) i am using this function i created to face the target because the dodging wont work with face target and we can customize how well the ai can track the player

        if (readyToAttack && !meleeAttacker)
        {
            myAnimator.SetTrigger("Attack");

            firePosition.LookAt(player);

            Instantiate(attackProjectile, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }

        else if (readyToAttack && meleeAttacker)
        {
            myAnimator.SetTrigger("Attack");
        }
    }

    private void DodgingAndAttackingPlayer()    // Functionality for dodging and attacking player at the same time
    {
        myAgent.SetDestination(gameObject.transform.position);
        FaceTarget(); //Instead of using transform.lookat(player) i am using this function i created to face the target because the dodging wont work with face target and we can customize how well the ai can track the player

        if (readyToAttack)
        {
            myAnimator.SetTrigger("Attack");

            firePosition.LookAt(player);

            Instantiate(attackProjectile, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }

        if (startDodgeDelay)
        {
            time = time + 1f * Time.deltaTime;
        }

        if (time >= dodgeDelay)
        {
            dodgeDelay = Random.Range(minDodgeTime, maxDodgeTime);
            time = 0;
        }

        if (time <= dodgeDelay / 2 && startDodgeDelay)
        {
            rb.AddForce(-transform.right * dodgeSpeed * Time.deltaTime);
        }
        else if (time >= dodgeDelay / 2 && startDodgeDelay)
        {
            rb.AddForce(transform.right * dodgeSpeed * Time.deltaTime);
        }
    }   

    private void ResetDodging()
    {
        time = 0;
        startDodgeDelay = false;
    }   //resets the dodging time and stops the enemy from dodging

    public void MeleeDamage()
    {
        if (playerInAttackRange)
        {
            player.GetComponent<PlayerHealthSystem>().TakeDamage(meleeDamageAmount);
        }
    }

    private void SearchForDestination()
    {
        // creates a randomized point for SmartEnemy NavMeshAgent to walk to
        float randPositionZ = Random.Range(-destinationRange, destinationRange);
        float randPositionX = Random.Range(-destinationRange, destinationRange);

        // set the destination
        destinationPoint = new Vector3(
            transform.position.x + randPositionX,
            transform.position.y,
            transform.position.z + randPositionZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
        {
            destinationSet = true;
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);

        readyToAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, destinationRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, dodgeRange);
    }

    void FaceTarget() //Instead of using transform.lookat(player) i am using this function i created to face the target because the dodging wont work with face target and we can customize how well the ai can track the player
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }   
}

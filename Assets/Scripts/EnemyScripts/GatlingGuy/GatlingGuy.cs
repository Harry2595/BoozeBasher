using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatlingGuy : MonoBehaviour
{
    //Stats
    [Range(0, 100)] public float speed;
    [Range(0, 500)] public float walkRadius;
    [Range(0, 100)] public float stopShootRadius;
    [Range(0, 100)] public float shootRadius;
    [Range(0, 100)] public float detectRadius;
    [Range(0, 1000)] public float dodgeSpeed;


    //Bools
    public bool isChasing;
    public bool isDead = false;

    bool startDelay = false;

    //Physics
    Rigidbody rb;

    //Time
    float time;
    public float timeDelay;

    Transform target;
    NavMeshAgent agent;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();

        time = 1;
        timeDelay = Random.Range(1f, 6f);

        if (agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    void Update()
    {
        if (isDead != true)
        {
            if (agent != null && agent.remainingDistance <= agent.stoppingDistance && isChasing != true)
            {
                agent.SetDestination(RandomNavMeshLocation());
            }

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= detectRadius)
            {
                isChasing = true;

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }

                if (distance <= shootRadius && distance >= stopShootRadius)
                {
                    //Shoot player
                }

                if (distance <= stopShootRadius)
                {
                    startDelay = true;
                    FaceTarget();
                    agent.SetDestination(gameObject.transform.position);

                    //Shoot player
                }
                else
                {
                    time = 0;
                    startDelay = false;
                    agent.SetDestination(target.position);
                }
            }
            else
            {
                isChasing = false;
            }
        }
        else
        {
            agent.SetDestination(gameObject.transform.position);
        }

        if (startDelay)
        {
            time = time + 1f * Time.deltaTime;
        }

        if (time >= timeDelay)
        {
            timeDelay = Random.Range(1f, 6f);
            time = 0;
        }

        if (time <= timeDelay / 2 && startDelay)
        {
            rb.AddForce(-transform.right * dodgeSpeed * Time.deltaTime);
        }
        else if (time >= timeDelay / 2 && startDelay)
        {
            rb.AddForce(transform.right * dodgeSpeed * Time.deltaTime);
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopShootRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}

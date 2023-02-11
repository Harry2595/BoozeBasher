using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProspecterFunctionality : MonoBehaviour
{
    //Stats
    [Range(0, 100)] public float speed;
    [Range(0, 500)] public float walkRadius;
    [Range(0, 100)] public float shootRadius;
    [Range(0, 100)] public float detectRadius;

    //Bools
    public bool isChasing;
    public bool isDead = false;

    bool startDelay = false;
    bool startRapidFire = false;

    bool startRapidFireLengthDelay;

    //Physics
    Rigidbody rb;

    //Time
    float time;
    public float timeDelay;

    float rapidFireTime;
    float rapidFireDelay = 0.3f;

    float rapidFireLastingTime;
    float rapidFireLastingTimeDelay = 5;

    Transform target;
    NavMeshAgent agent;

    //Attacks
    public int attackNum;

    //Spawns
    public GameObject bomb;
    public GameObject bombSpawn;

    public GameObject shotgunSpawn1;
    public GameObject shotgunSpawn2;
    public GameObject shotgunSpawn3;


    //BoomLaunchStuff
    public float throwSpeed;
    float rotationSpeed;
    float turnSpeed;
    float defaultThrowSpeed;

    float shotgunTurnSpeed1;
    float shotgunTurnSpeed2;
    float shotgunTurnSpeed3;

    float shotgunRotationSpeed1;
    float shotgunRotationSpeed2;
    float shotgunRotationSpeed3;

    //Time
    float time2;
    public float throwTimeDelay;

    //Bools
    bool startThrowDelay = true;

    bool typeDecided = false;
    public int fireType;

    //Rigidbodies
    public Rigidbody boomRB;

    public Rigidbody shotgunBoomRB1;
    public Rigidbody shotgunBoomRB2;
    public Rigidbody shotgunBoomRB3;

    //TempRigidbody
    public GameObject tempRB;

    //Other
    public static ProspecterFunctionality instance;
    public GameObject tracker;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        defaultThrowSpeed = throwSpeed;

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();

        time = 1;
        timeDelay = Random.Range(0.5f, 4f);

        time2 = 1;
        throwTimeDelay = Random.Range(0.5f, 2f);

        rapidFireTime = 1;

        rapidFireLastingTime = 1;

        attackNum = Random.Range(1, 10);
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

                if (distance <= shootRadius && time2 >= throwTimeDelay && attackNum != 5 && attackNum != 4)
                {
                    //ThrowBomb

                        throwSpeed = throwSpeed * distance / 8;

                        GameObject bombClone = Instantiate(bomb, bombSpawn.transform.position, Quaternion.identity);
                        boomRB = bombClone.GetComponent<Rigidbody>();
                        boomRB.AddForce(tracker.transform.forward * 20 * throwSpeed, ForceMode.Force);

                        turnSpeed = boomRB.velocity.magnitude;
                        rotationSpeed = boomRB.angularVelocity.magnitude * Mathf.Rad2Deg;
                        boomRB.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);

                        time2 = 0;
                        startThrowDelay = true;

                        throwTimeDelay = Random.Range(0.5f, 2f);
                        throwSpeed = defaultThrowSpeed;

                        attackNum = Random.Range(1, 10);
                }

                if (distance <= shootRadius && time2 >= throwTimeDelay && attackNum == 5)
                {
                    //RapidFireNormal/Volcano

                    if(typeDecided != true)
                    {
                        fireType = Random.Range(1, 3);
                        typeDecided = true;
                    }

                    startRapidFireLengthDelay = true;

                    if(rapidFireLastingTime <= rapidFireLastingTimeDelay)
                    {
                        startRapidFire = true;
                        if(rapidFireTime >= rapidFireDelay)
                        {
                            throwSpeed = throwSpeed * distance / 8;

                            GameObject bombClone = Instantiate(bomb, bombSpawn.transform.position, Quaternion.identity);
                            boomRB = bombClone.GetComponent<Rigidbody>();

                            if(fireType == 1)
                            {
                                boomRB.AddForce(tracker.transform.forward * 20 * throwSpeed, ForceMode.Force);
                            }
                            else
                            {
                                boomRB.AddForce(tracker.transform.up * 15 * throwSpeed, ForceMode.Force);
                                boomRB.AddForce(tracker.transform.forward * 4 * throwSpeed, ForceMode.Force);
                            }

                            turnSpeed = boomRB.velocity.magnitude;
                            rotationSpeed = boomRB.angularVelocity.magnitude * Mathf.Rad2Deg;
                            boomRB.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);
                            throwSpeed = defaultThrowSpeed;
                            rapidFireTime = 0;
                        }
                    }
                    else if(rapidFireLastingTime > rapidFireLastingTimeDelay)
                    {
                        typeDecided = false;
                        startRapidFireLengthDelay = false;
                        rapidFireLastingTime = 0;
                        rapidFireTime = 0;

                        time2 = 0;
                        startThrowDelay = true;

                        throwTimeDelay = Random.Range(0.5f, 2f);
                        throwSpeed = defaultThrowSpeed;

                        attackNum = 1; 
                    }
                }

                if (distance <= shootRadius && time2 >= throwTimeDelay && attackNum == 4)
                {
                    //BoomShotgun

                    throwSpeed = throwSpeed * distance / 8;

                    GameObject bombClone1 = Instantiate(bomb, shotgunSpawn1.transform.position, Quaternion.identity);
                    GameObject bombClone2 = Instantiate(bomb, shotgunSpawn2.transform.position, Quaternion.identity);
                    GameObject bombClone3 = Instantiate(bomb, shotgunSpawn3.transform.position, Quaternion.identity);

                    shotgunBoomRB1 = bombClone1.GetComponent<Rigidbody>();
                    shotgunBoomRB2 = bombClone2.GetComponent<Rigidbody>();
                    shotgunBoomRB3 = bombClone3.GetComponent<Rigidbody>();

                    shotgunBoomRB1.AddForce(tracker.transform.forward * 20 * throwSpeed, ForceMode.Force);
                    shotgunBoomRB2.AddForce(tracker.transform.forward * 20 * throwSpeed, ForceMode.Force);
                    shotgunBoomRB3.AddForce(tracker.transform.forward * 20 * throwSpeed, ForceMode.Force);

                    shotgunTurnSpeed1 = shotgunBoomRB1.velocity.magnitude;
                    shotgunRotationSpeed1 = shotgunBoomRB1.angularVelocity.magnitude * Mathf.Rad2Deg;
                    shotgunBoomRB1.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);

                    shotgunTurnSpeed2 = shotgunBoomRB2.velocity.magnitude;
                    shotgunRotationSpeed2 = shotgunBoomRB2.angularVelocity.magnitude * Mathf.Rad2Deg;
                    shotgunBoomRB2.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);

                    shotgunTurnSpeed3 = shotgunBoomRB3.velocity.magnitude;
                    shotgunRotationSpeed3 = shotgunBoomRB3.angularVelocity.magnitude * Mathf.Rad2Deg;
                    shotgunBoomRB3.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);

                    time2 = 0;
                    startThrowDelay = true;

                    throwTimeDelay = Random.Range(0.5f, 2f);
                    throwSpeed = defaultThrowSpeed;

                    attackNum = Random.Range(1, 10);
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

        if (startThrowDelay)
        {
            time2 = time2 + 1f * Time.deltaTime;
        }

        if (startRapidFire)
        {
            rapidFireTime = rapidFireTime + 1f * Time.deltaTime;
        }

        if (startRapidFireLengthDelay)
        {
            rapidFireLastingTime = rapidFireLastingTime + 1f * Time.deltaTime;
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

    public void AssignRB()
    {
        rb = tempRB.GetComponent<Rigidbody>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KniferAI : MonoBehaviour
{
    //Stats
    [Range(0, 100)] public float speed;
    [Range(0, 500)] public float walkRadius;
    public float detectRadius = 10f;

    //Bools
    public bool isChasing;
    public bool isDead = false;

    Transform target;
    NavMeshAgent agent;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        if(agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    void Update()
    {
        if(isDead != true)
        {
            if (agent != null && agent.remainingDistance <= agent.stoppingDistance && isChasing != true)
            {
                agent.SetDestination(RandomNavMeshLocation());
            }

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= detectRadius)
            {
                isChasing = true;
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
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
    }
}

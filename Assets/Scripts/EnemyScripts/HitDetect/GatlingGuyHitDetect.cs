using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatlingGuyHitDetect : MonoBehaviour
{
    public float health = 50f;

    public GatlingGuy GGRef;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        GGRef.isDead = true;
        GGRef.isChasing = false;
        GGRef.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        Destroy(gameObject, 2);
    }
}

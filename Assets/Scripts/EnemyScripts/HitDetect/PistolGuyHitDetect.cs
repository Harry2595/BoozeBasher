using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PistolGuyHitDetect : MonoBehaviour
{
    public float health = 50f;

    public PistolGuy PGRef;

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
        PGRef.isDead = true;
        PGRef.isChasing = false;
        PGRef.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        Destroy(gameObject, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RepeaterGuyHitDetect : MonoBehaviour
{
    public float health = 70f;

    public RepeaterGuy RGRef;

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
        RGRef.isDead = true;
        RGRef.isChasing = false;
        RGRef.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        Destroy(gameObject, 2);
    }
}

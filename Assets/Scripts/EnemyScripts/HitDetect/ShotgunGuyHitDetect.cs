using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShotgunGuyHitDetect : MonoBehaviour
{
    public float health = 40f;

    public ShotgunGuy SGRef;

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
        SGRef.isDead = true;
        SGRef.isChasing = false;
        SGRef.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        Destroy(gameObject, 2);
    }
}

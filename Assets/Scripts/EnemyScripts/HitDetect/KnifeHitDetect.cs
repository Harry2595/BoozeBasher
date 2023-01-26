using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitDetect : MonoBehaviour
{
    public float health = 50f;

    public KniferAI KARef;

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
        KARef.isDead = true;
        KARef.isChasing = false;
        Destroy(gameObject, 2);
    }
}

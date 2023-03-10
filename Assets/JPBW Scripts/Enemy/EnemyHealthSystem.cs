using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int currentHealth = 5;

    EnemyManager EMRef;
    
    void Start()
    {
        EMRef = FindObjectOfType<EnemyManager>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <=0)
        {
            EMRef.enemiesSpawned--;
            Destroy(gameObject);
        }
    }

    public void AssignHealth(int HPGain)   //Called on by the enemy manager when an enemy is spawned in
    {
        currentHealth += HPGain;
    }
}

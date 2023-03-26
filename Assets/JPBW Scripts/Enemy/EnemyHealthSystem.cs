using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int currentHealth = 5;

    public GameObject HPPowerUp;
    public GameObject DamagePowerUp;

    int rand1;

    EnemyManager EMRef;
    
    void Start()
    {
        EMRef = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        rand1 = Random.Range(1, 30);
        Debug.Log(rand1);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <=0)
        {
            EMRef.enemiesSpawned--;
            if (rand1 == 1)
            {
                Instantiate(HPPowerUp, this.transform.position, Quaternion.identity);
            }
            else if (rand1 == 2)
            {
                Instantiate(DamagePowerUp, this.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void AssignHealth(int HPGain)   //Called on by the enemy manager when an enemy is spawned in
    {
        currentHealth += HPGain;
    }
}

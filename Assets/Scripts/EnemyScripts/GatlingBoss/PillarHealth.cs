using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHealth : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;

    GatlingBoss GBRef;

    void Start()
    {
        currentHealth = startingHealth;
        GBRef = GatlingBoss.instance.GetComponent<GatlingBoss>();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            GBRef.currentPillarDeaths++;
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = currentHealth - dmg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public GameObject Spawn;

    public LevelManager LMRef; 

    UICanvasController healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar = FindObjectOfType<UICanvasController>();
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);  // moved from take damage function to here so it also updates when the player gains health
    }


    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;

        if(currentHealth <= 0)
        {
            LMRef.ResetAllRounds();
            gameObject.transform.position = Spawn.transform.position;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void PowerUpMaxHP(int HPIncrease)
    {
        maxHealth += HPIncrease;
        currentHealth = maxHealth;
    }
}

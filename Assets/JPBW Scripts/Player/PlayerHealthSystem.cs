using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    UICanvasController healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar = FindObjectOfType<UICanvasController>();
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerRespawn();
        }
    }

    public void PowerUpMaxHP(int HPIncrease)
    {
        maxHealth += HPIncrease;
        currentHealth = maxHealth;
    }
}

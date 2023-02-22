using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int round;
    public bool canStartRound = true;
    public bool canExitRound = false;
    public int pointGain;
    public bool roundInProgress = false;
    bool destroyEnemies = false;

    public int healthGain;
    public int damageGain;

    //refs
    public EnemyManager EMRef;

    GameObject[] enemies;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (destroyEnemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            destroyEnemies = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("roundEnter") && canStartRound)
        {
            StartRound();
        }

        if (other.gameObject.CompareTag("roundEnd/Exit"))
        {
            if (EMRef.canLeave)
            {
                EndRound();
            }
            else if (canExitRound)
            {
                ExitRound();
            }
        }
    }

    void StartRound()               //starts the round and increases the round counter by 1 as well as the health and damage gain for the enemies
    {
        roundInProgress = true;
        Debug.Log("RoundStart");
        canExitRound = true;
        EMRef.canSpawnEnemies = true;
        EMRef.noMorePoints = false;
        canStartRound = false;
        round++;
        pointGain += 2;
        EMRef.canSpawnEnemies = true;
        EMRef.GivePoints();

        healthGain += 2;
        damageGain += 2;
    }

    void EndRound()              //The player can end the round when they have killed all enemies and there are no more points for the enemy manager to use
    {
        roundInProgress = false;
        Debug.Log("RoundEnd");
        canStartRound = true;
    }

    void ExitRound()            //the player can exit the round at any time which will cause the round to reset 
    {
        destroyEnemies = true;
        roundInProgress = false;
        Debug.Log("ExitRound");
        canExitRound = false;
        canStartRound = true;
        EMRef.ResetRound();
        pointGain -= 2;
        healthGain -= 2;
        damageGain -= 2;
        round--;
    }
}

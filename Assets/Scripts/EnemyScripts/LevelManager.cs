using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int round;
    public bool canStartRound = true;
    public bool canExitRound = false;
    public bool roundInProgress = false;
    bool destroyEnemies = false;

    public int pointGain;
    public int healthGain;
    public int damageGain;

    int startingPointGain;
    int startingHealthGain;
    int startingDamageGain;

    public TMP_Text roundText;

    //refs
    public EnemyManager EMRef;

    public GunSystem GS1Ref;
    public GunSystem GS2Ref;
    public GunSystem GS3Ref;

    GameObject[] enemies;

    void Start()
    {
        startingPointGain = pointGain;
    }

    void Update()
    {
        roundText.text = round.ToString();

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
        GS1Ref.canShoot = true;
        GS2Ref.canShoot = true;
        GS3Ref.canShoot = true;

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
        GS1Ref.canShoot = false;
        GS2Ref.canShoot = false;
        GS3Ref.canShoot = false;

        roundInProgress = false;
        canStartRound = true;
    }

    void ExitRound()            //the player can exit the round at any time which will cause the round to reset 
    {
        GS1Ref.canShoot = false;
        GS2Ref.canShoot = false;
        GS3Ref.canShoot = false;

        destroyEnemies = true;
        roundInProgress = false;
        canExitRound = false;
        canStartRound = true;
        EMRef.ResetRound();
        pointGain -= 2;
        healthGain -= 2;
        damageGain -= 2;
        round--;
    }

    public void ResetAllRounds()      //Resets everything when the player dies
    {
        GS1Ref.canShoot = false;
        GS2Ref.canShoot = false;
        GS3Ref.canShoot = false;

        destroyEnemies = true;
        roundInProgress = false;
        canExitRound = false;
        canStartRound = true;

        pointGain = startingPointGain;
        healthGain = startingHealthGain;
        damageGain = startingDamageGain;
        EMRef.ResetRound();

        round = 0;
    }
}

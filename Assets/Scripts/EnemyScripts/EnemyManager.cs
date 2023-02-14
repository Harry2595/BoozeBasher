using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Vars
    float enemyHealth; 
    float enemyDamage; 
    float healthMultiplier = 1.1f; 
    float damageMultiplier = 1.05f; 
    float maxPointsPerRound; 
    float maxEnemiesSpawnedAtOnce = 20f; 
    public float enemiesSpawned; 
    public float point;
    float startingPoints;

    //float round; 
    public int currentPoints; 

    //Prices
    int kniferPrice = 2; 
    int pistolPrice = 4; 
    int repeaterPrice = 6; 
    int shotgunPrice = 8; 
    int gatlingPrice = 10;

    //Refs
    public LevelManager LMRef;

    //Bools
    public bool canSpawnEnemies = false;

    bool canSpawnKnifer = false; 
    bool canSpawnPistol = false; 
    bool canSpawnRepeater = false; 
    bool canSpawnShotgun = false; 
    bool canSpawnGat = false;

    public bool noMorePoints = false;
    bool startTimer = false;

    public bool canLeave = false;

    //PreFabs
    public GameObject knifer;
    public GameObject pistolGuy;
    public GameObject repeaterGuy;
    public GameObject shotgunGuy;
    public GameObject gatGuy;

    //Spawns
    public Transform spawn1;
    public Transform spawn2;

    //Rands
    int ID;
    int spawnID;

    //Time
    float time;
    float spawnDelay;


    void Start()
    {
        time = 1;
        spawnDelay = 2f;

        startTimer = true;

        enemiesSpawned = 0;
    }

    void Update()
    {
        //Debug.Log(ID);

        if (enemiesSpawned < maxEnemiesSpawnedAtOnce && noMorePoints != true && time >= spawnDelay && canSpawnEnemies)
        {
            SpawnEnemy();
            time = 0;
            startTimer = true;
        }

        if (currentPoints <= 1 && enemiesSpawned <= 0)
        {
            canSpawnEnemies = false;
            noMorePoints = true;
        }

        if(enemiesSpawned <= 0 && LMRef.roundInProgress && currentPoints <= 1)
        {
            canLeave = true;
            LMRef.canExitRound = false;
        }

        if (startTimer)
        {
            time = time + 1f * Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        
        if (enemiesSpawned <= maxEnemiesSpawnedAtOnce - 1)
        {
            if(currentPoints >= kniferPrice)
            {
                canSpawnKnifer = true;
            }
            else
            {
                canSpawnKnifer = false;
            }

            if (currentPoints >= pistolPrice)
            {
                canSpawnPistol = true;
            }
            else
            {
                canSpawnPistol = false;
            }

            if (currentPoints >= repeaterPrice)
            {
                canSpawnRepeater = true;
            }
            else
            {
                canSpawnRepeater = false;
            }

            if (currentPoints >= shotgunPrice)
            {
                canSpawnShotgun = true;
            }
            else
            {
                canSpawnShotgun = false;
            }

            if (currentPoints >= gatlingPrice)
            {
                canSpawnGat = true;
            }
            else
            {
                canSpawnGat = false;
            }
        }

        if(currentPoints >= kniferPrice && enemiesSpawned <= maxEnemiesSpawnedAtOnce - 1)
        {
            if (canSpawnKnifer && canSpawnPistol && canSpawnRepeater && canSpawnShotgun && canSpawnGat) 
            {
                ID = Random.Range(1, 6);
                spawnID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if(spawnID == 1)
                        {
                            Instantiate(knifer, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(knifer, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 2:
                        if (spawnID == 1)
                        {
                            Instantiate(pistolGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 3:
                        if (spawnID == 1)
                        {
                            Instantiate(repeaterGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 4:
                        if (spawnID == 1)
                        {
                            Instantiate(shotgunGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - shotgunPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(shotgunGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - shotgunPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 5:
                        if (spawnID == 1)
                        {
                            Instantiate(gatGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - gatlingPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(gatGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - gatlingPrice;
                            enemiesSpawned++;
                        }

                        break;
                }
            }
            else if(canSpawnKnifer && canSpawnPistol && canSpawnRepeater && canSpawnShotgun && canSpawnGat)
            {
                ID = Random.Range(1, 5);
                spawnID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (spawnID == 1)
                        {
                            Instantiate(knifer, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(knifer, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 2:
                        if (spawnID == 1)
                        {
                            Instantiate(pistolGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 3:
                        if (spawnID == 1)
                        {
                            Instantiate(repeaterGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 4:
                        if (spawnID == 1)
                        {
                            Instantiate(shotgunGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - shotgunPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(shotgunGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - shotgunPrice;
                            enemiesSpawned++;
                        }

                        break;
                }
            }
            else if(canSpawnKnifer && canSpawnPistol && canSpawnRepeater && canSpawnShotgun)
            {
                ID = Random.Range(1, 4);
                spawnID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (spawnID == 1)
                        {
                            Instantiate(knifer, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(knifer, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 2:
                        if (spawnID == 1)
                        {
                            Instantiate(pistolGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 3:
                        if (spawnID == 1)
                        {
                            Instantiate(repeaterGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - repeaterPrice;
                            enemiesSpawned++;
                        }

                        break;
                }
            }
            else if(canSpawnKnifer && canSpawnPistol)
            {
                ID = Random.Range(1, 3);
                spawnID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (spawnID == 1)
                        {
                            Instantiate(knifer, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(knifer, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - kniferPrice;
                            enemiesSpawned++;
                        }

                        break;

                    case 2:
                        if (spawnID == 1)
                        {
                            Instantiate(pistolGuy, spawn1.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, spawn2.transform.position, Quaternion.identity);
                            currentPoints = currentPoints - pistolPrice;
                            enemiesSpawned++;
                        }

                        break;
                }
            }
            else if(canSpawnKnifer)
            {
                spawnID = Random.Range(1, 3);

                Instantiate(knifer, spawn1.transform.position, Quaternion.identity);
                currentPoints = currentPoints - kniferPrice;
                enemiesSpawned++;
            }


        }
    }

    public void GivePoints()
    { 
        currentPoints = 100 * LMRef.pointGain - 40 * LMRef.pointGain;
        startingPoints = currentPoints;
    }

    public void ResetRound()
    {
        currentPoints = 0;

        canSpawnEnemies = false;
        canSpawnKnifer = false;
        canSpawnPistol = false;
        canSpawnRepeater = false;
        canSpawnShotgun = false;
        canSpawnGat = false;

        enemiesSpawned = 0;
    }
}

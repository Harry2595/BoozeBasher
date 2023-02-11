using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Vars
    float EH; //enemyHealth
    float ED; //enemyDamage
    float HM = 1.1f; //healthMultiplier
    float DM = 1.05f; //damageMultiplier
    float MPPR; //maxPointsPerRound
    float MES = 20f; //maxEnemiesSpawnedAtOnce
    public float ES; //enemiesSpawned
    float PG = 1.5f; //pointGain
    float P = 1f; //point
    float R; //round
    public float CP; //currentPoints

    //Prices
    float KP = 2; //knifer price
    float PP = 4; //knifer price
    float RP = 6; //knifer price
    float SP = 8; //knifer price
    float GP = 10; //knifer price

    //Refs

    //Bools
    bool CSK = false; //canSpawnKnifer
    bool CSP = false; //canSpawnPistolGuy
    bool CSR = false; //canSpawnRepeaterGuy
    bool CSS = false; //canSpawnShotgunGuy
    bool CSG = false; //canSpawnGatGuy

    bool NMP = false; //noMorePoints;
    bool startTimer = false;

    //PreFabs
    public GameObject knifer;
    public GameObject pistolGuy;
    public GameObject repeaterGuy;
    public GameObject shotgunGuy;
    public GameObject gatGuy;

    //Spawns
    public Transform S1; //Spawn1
    public Transform S2; //Spawn2

    //Rands
    int ID;
    int SID; //SpawnID

    //Time
    float time;
    float spawnDelay;


    void Start()
    {
        time = 1;
        spawnDelay = 2f;

        startTimer = true;

        ES = 0;
        CP = 1000;
    }

    void Update()
    {
        //Debug.Log(ID);

        if (ES < MES && NMP != true && time >= spawnDelay)
        {
            SpawnEnemy();
            time = 0;
            startTimer = true;
        }

        if (CP <= 1 && ES <= 0)
        {
            Debug.Log("No More Points");
            NMP = true;
        }

        if(ES <= 0)
        {
            //Player can leave and when they come back in, round num will increase
        }

        if (startTimer)
        {
            time = time + 1f * Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        
        if (ES <= MES - 1)
        {
            if(CP >= KP)
            {
                CSK = true;
            }
            else
            {
                CSK = false;
            }

            if (CP >= PP)
            {
                CSP = true;
            }
            else
            {
                CSP = false;
            }

            if (CP >= RP)
            {
                CSR = true;
            }
            else
            {
                CSR = false;
            }

            if (CP >= SP)
            {
                CSS = true;
            }
            else
            {
                CSS = false;
            }

            if (CP >= GP)
            {
                CSG = true;
            }
            else
            {
                CSG = false;
            }
        }

        if(CP >= KP && ES <= MES - 1)
        {
            if (CSK && CSP && CSR && CSS && CSG) 
            {
                ID = Random.Range(1, 6);
                SID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if(SID == 1)
                        {
                            Instantiate(knifer, S1.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(knifer, S2.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }

                        break;

                    case 2:
                        if (SID == 1)
                        {
                            Instantiate(pistolGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }

                        break;

                    case 3:
                        if (SID == 1)
                        {
                            Instantiate(repeaterGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }

                        break;

                    case 4:
                        if (SID == 1)
                        {
                            Instantiate(shotgunGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - SP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(shotgunGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - SP;
                            ES++;
                        }

                        break;

                    case 5:
                        if (SID == 1)
                        {
                            Instantiate(gatGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - GP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(gatGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - GP;
                            ES++;
                        }

                        break;
                }
            }
            else if(CSK && CSP && CSR && CSS && CSG)
            {
                ID = Random.Range(1, 5);
                SID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (SID == 1)
                        {
                            Instantiate(knifer, S1.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(knifer, S2.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }

                        break;

                    case 2:
                        if (SID == 1)
                        {
                            Instantiate(pistolGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }

                        break;

                    case 3:
                        if (SID == 1)
                        {
                            Instantiate(repeaterGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }

                        break;

                    case 4:
                        if (SID == 1)
                        {
                            Instantiate(shotgunGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - SP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(shotgunGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - SP;
                            ES++;
                        }

                        break;
                }
            }
            else if(CSK && CSP && CSR && CSS)
            {
                ID = Random.Range(1, 4);
                SID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (SID == 1)
                        {
                            Instantiate(knifer, S1.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(knifer, S2.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }

                        break;

                    case 2:
                        if (SID == 1)
                        {
                            Instantiate(pistolGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }

                        break;

                    case 3:
                        if (SID == 1)
                        {
                            Instantiate(repeaterGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(repeaterGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - RP;
                            ES++;
                        }

                        break;
                }
            }
            else if(CSK && CSP)
            {
                ID = Random.Range(1, 3);
                SID = Random.Range(1, 3);

                switch (ID)
                {
                    case 1:
                        if (SID == 1)
                        {
                            Instantiate(knifer, S1.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(knifer, S2.transform.position, Quaternion.identity);
                            CP = CP - KP;
                            ES++;
                        }

                        break;

                    case 2:
                        if (SID == 1)
                        {
                            Instantiate(pistolGuy, S1.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }
                        else
                        {
                            Instantiate(pistolGuy, S2.transform.position, Quaternion.identity);
                            CP = CP - PP;
                            ES++;
                        }

                        break;
                }
            }
            else if(CSK)
            {
                SID = Random.Range(1, 3);

                Instantiate(knifer, S1.transform.position, Quaternion.identity);
                CP = CP - KP;
                ES++;
            }


        }
    }

    void StartRound()
    {
        //getRoundNum
        //Communicate to enemies how much health they should have if this script needs to
    }
}

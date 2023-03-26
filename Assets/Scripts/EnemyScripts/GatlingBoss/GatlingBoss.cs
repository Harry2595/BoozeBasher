using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBoss : MonoBehaviour
{
    Transform player;
    public Transform firePosition;

    //Projectile
    public float attackTime;
    public GameObject attackProjectile;
    public bool readyToAttack = true;

    //MegaAttack
    float time = 1;
    float megaTime = 1;
    float megaAttackDelay = 25;
    float megaAttackTime = 10;
    bool startMegaTimer = false;

    //OverheatCooldown
    bool overHeating = false;
    float coolDownTime = 15;

    //Pillars
    int pillarDeathsNeeded = 6;
    public float spawnShiftX1 = 0; //affects spawn split on the x-axis
    public float spawnShiftX2 = 4; //affects spawn shift on the x-axis
    public float spawnShiftZ1 = 9; //affects spawn shift on the z-axis
    public float spawnShiftZ2 = 15; //affects spawn split on the z-axis
    public int currentPillarDeaths;
    public float spawnRadius;
    public float minSpawnRadius;
    public GameObject pillar;

    public static GatlingBoss instance;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        SpawnPillars();
    }

    void Update()
    {
        if (overHeating != true)
        {
            time = time + 1f * Time.deltaTime;
            FaceTarget();
        }

        if(time >= megaAttackDelay)
        {
            MegaAttack();
            startMegaTimer = true;
        }
        else
        {
            if (readyToAttack)
            {
                Attack();
            }
        }

        if (startMegaTimer)
        {
            megaTime = megaTime + 1f * Time.deltaTime;
        }

        if (megaTime >= megaAttackTime)
        {
            megaTime = 0;
            time = 0;
            startMegaTimer = false;

            overHeating = true;
            readyToAttack = false;
            StartCoroutine(OverheatTimer());
        }
    }

    void Attack()
    {
        if (readyToAttack)
        {
            firePosition.LookAt(player);

            Instantiate(attackProjectile, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }
    }

    void MegaAttack()
    {
        if (readyToAttack)
        {
            firePosition.LookAt(player);

            Instantiate(attackProjectile, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }
    }

    void FaceTarget() 
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void SpawnPillars()
    {
        Vector3 randomPos;

        for(int i = 0; i < 6; i++)
        {
            do
            {
                randomPos = Random.insideUnitSphere * spawnRadius + new Vector3(0, 0, -10);
            } while (randomPos.x <= minSpawnRadius - spawnShiftX1 && randomPos.x >= minSpawnRadius - minSpawnRadius - spawnShiftX2 || randomPos.z <= minSpawnRadius - spawnShiftZ1 && randomPos.z >= minSpawnRadius - minSpawnRadius - spawnShiftZ2); 


            Instantiate(pillar, randomPos, Quaternion.identity);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, minSpawnRadius);
    }

    IEnumerator ResetAttack()
    {
        if(time >= megaAttackDelay)
        {
            yield return new WaitForSeconds(attackTime / 4);
        }
        else
        {
            yield return new WaitForSeconds(attackTime);
        }
        if(overHeating != true)
        {
            readyToAttack = true;
        }
    }

    IEnumerator OverheatTimer()
    {
        yield return new WaitForSeconds(coolDownTime);

        overHeating = false;
        readyToAttack = true;
    }
}

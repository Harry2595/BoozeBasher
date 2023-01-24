using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActionScript : MonoBehaviour
{
    //Stats
    public float damage;
    public float range;

    //Effects
    public ParticleSystem bloodSplat;

    //Bools
    bool StartFireDelay = false;
    bool Holding = false;

    //Time
    float time;
    float timeDelay = 1f;

    //Animation
    [SerializeField] private Animator leverAction;
    [SerializeField] private string rCock = "RepeaterCock";

    //Refs
    public Camera fpsCam;
    public WeponManager WM;


    void Start()
    {
        time = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Holding = true;
            if (time >= timeDelay)
            {
                Shoot();
                leverAction.Play(rCock);

                time = 0;
                StartFireDelay = true;
            }

        }

        if (Input.GetButtonUp("Fire1"))
        {
            Holding = false;
        }

        if (Holding)
        {
            if (time >= timeDelay)
            {
                Shoot();
                leverAction.Play(rCock);

                time = 0;
                StartFireDelay = true;
            }
        }

        if (StartFireDelay)
        {
            time = time + 1f * Time.deltaTime;
        }
    }

    void Shoot()
    {
        //HitDetection
        if (WM.hasLeverAction && WM.leverActionOut)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                BanditHitDetect Bandit = hit.transform.GetComponent<BanditHitDetect>(); //Checking if the object we hit has a specific script
                if (Bandit != null)
                {
                    ParticleSystem tempBlood = Instantiate(bloodSplat, hit.point, Quaternion.identity);
                    tempBlood.Play();
                    Bandit.TakeDamage(damage);
                    Destroy(tempBlood, 1); //it wont get rid of clone...
                }
            }
        }
    }
}

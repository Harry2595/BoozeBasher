using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //KnifeStats
    public float damage;
    public float range;

    //Effects
    public ParticleSystem bloodSplat;

    //Bools
    bool StartFireDelay = false;
    bool Holding = false;

    //Time
    float time;
    float timeDelay = 0.8f;

    //Animation
    [SerializeField] private Animator knife;
    [SerializeField] private string stab1 = "Stab1";

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
                time = 0;
                knife.Play(stab1);

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
                time = 0;
                knife.Play(stab1);

                StartFireDelay = true;
            }
        }

        if (knife.GetCurrentAnimatorStateInfo(0).IsName("Stab1"))
        {
            Stab();
        }

        if (StartFireDelay)
        {
            time = time + 1f * Time.deltaTime;
        }
    }

    void Stab()
    {
        //HitDetection

        if (WM.hasKnife && WM.knifeOut)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                KnifeHitDetect Bandit = hit.transform.GetComponent<KnifeHitDetect>(); //Checking if the object we hit has a specific script
                if (Bandit != null)
                {
                    ParticleSystem tempBlood = Instantiate(bloodSplat, hit.point, Quaternion.identity);
                    tempBlood.Play();
                    Bandit.TakeDamage(damage);
                }
            }
        }
    }
}

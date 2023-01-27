using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponManager : MonoBehaviour
{
    //Melee Bools
    public bool hasKnife = false;
    public bool hasMachete = false;
    public bool knifeOut = false;
    public bool macheteOut = false;
    //Pistol Bools
    public bool hasPistol = false;
    public bool pistolOut = false;

    //Dynimite Bools/Nums
    public int boomNums = 0;
    public bool boomOut = false;

    //LeverAction Bools
    public bool hasLeverAction = false;
    public bool leverActionOut = false;

    //Melee GFX
    public GameObject knifeGFX;

    //Pistol GFX
    public GameObject pistolGFX;

    //Dynimite GFX
    public GameObject boomGFX;

    //LeverAction GFX
    public GameObject leverActionGFX;

    //Interactables
    public GameObject leverActionInteractable;
    public GameObject pistolInteractable;

    //Other
    public GameObject dropLocation;

    void Start()
    {
        pistolGFX.SetActive(false);
    }

    void Update()
    {
        WeaponSwitching();
    }


    public void WeaponSwitching()
    {
        if (Input.GetKeyDown("1"))
        {
            if (hasMachete)
            {
                //Machete On
                macheteOut = true;

                //Others Off
                pistolOut = false;
                pistolGFX.SetActive(false);
                knifeOut = false;
                knifeGFX.SetActive(false);
                boomOut = false;
                leverActionOut = false;
                leverActionGFX.SetActive(false);
            }
            else
            {
                //Knife On
                knifeOut = true;
                knifeGFX.SetActive(true);

                //Others Off
                pistolOut = false;
                pistolGFX.SetActive(false);
                macheteOut = false;
                boomOut = false;
                boomGFX.SetActive(false);
                leverActionOut = false;
                leverActionGFX.SetActive(false);
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            if (hasPistol)
            {
                //Pistol On
                pistolGFX.SetActive(true);
                pistolOut = true;

                //Others Off
                macheteOut = false;
                knifeOut = false;
                knifeGFX.SetActive(false);
                boomOut = false;
                boomGFX.SetActive(false);
                leverActionOut = false;
                leverActionGFX.SetActive(false);
            }

            if (hasLeverAction)
            {
                //Pistol On
                leverActionGFX.SetActive(true);
                leverActionOut = true;

                //Others Off
                pistolOut = false;
                pistolGFX.SetActive(false);
                macheteOut = false;
                knifeOut = false;
                knifeGFX.SetActive(false);
                boomOut = false;
                boomGFX.SetActive(false);
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            if(boomNums >= 1)
            {
                //Dynimite Out
                boomOut = true;
                boomGFX.SetActive(true);

                //Others Off
                pistolOut = false;
                pistolGFX.SetActive(false);
                leverActionOut = false;
                leverActionGFX.SetActive(false);
                knifeOut = false;
                knifeGFX.SetActive(false);
                macheteOut = false;
            }
        }
    }

    public void PistolPickup()
    {
        //Pickup
        hasPistol = true;
        pistolOut = true;
        pistolGFX.SetActive(true);

        //Drop
        if (hasLeverAction)
        {
            hasLeverAction = false;
            leverActionOut = false;
            leverActionGFX.SetActive(false);
            Instantiate(leverActionInteractable, dropLocation.transform.position, Quaternion.identity);
        }

        //Melee
        knifeOut = false;
        knifeGFX.SetActive(false);

        //Misc
        boomOut = false;
        boomGFX.SetActive(false);
    }

    public void LeverActionPickup()
    {
        //Pickup
        hasLeverAction = true;
        leverActionOut = true;
        leverActionGFX.SetActive(true);

        //Drop
        if (hasPistol)
        {
            hasPistol = false;
            pistolOut = false;
            pistolGFX.SetActive(false);
            Instantiate(pistolInteractable, dropLocation.transform.position, Quaternion.identity);
        }

        //Melee
        knifeOut = false;
        knifeGFX.SetActive(false);

        //Misc
        boomOut = false;
        boomGFX.SetActive(false);
    }

    public void KnifePickup()
    {
        //Pickup
        hasKnife = true;
        knifeOut = true;
        knifeGFX.SetActive(true);

        //Guns
        pistolGFX.SetActive(false);
        pistolOut = false;
        leverActionGFX.SetActive(false);
        leverActionOut = false;

        //Misc
        boomOut = false;
        boomGFX.SetActive(false);
    }

    public void BoomPickup()
    {
        boomNums++;
        if (boomOut)
        {
            boomGFX.SetActive(true);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    //Stats
    public float range = 100f;

    //GFX
    public Camera fpsCam;
    public GameObject defaultIcon;
    public GameObject hoverIcon;

    //Bools
    bool interacting = false;

    //Refs
    public WeponManager WM;

    void Start()
    {
        defaultIcon.SetActive(true);
        hoverIcon.SetActive(false);
    }

    void Update()
    {
            
        if (interacting != true)
        {
            defaultIcon.SetActive(true);
            hoverIcon.SetActive(false);
        }

        //RayCast
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                hoverIcon.SetActive(true);
                defaultIcon.SetActive(false);

                interacting = true;
            }
            else
            {
                interacting = false;  //improves fps
            }
        }
        else
        {
            interacting = false;  //improves fps
        }


        //Pickup
        if (Input.GetKeyDown("e"))
        {
            if (hit.transform.gameObject.CompareTag("Pistol"))
            {
                WM.PistolPickup();
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("LeverAction"))
            {
                WM.LeverActionPickup();
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Knife"))
            {
                WM.KnifePickup();
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Bomb"))
            {
                WM.BoomPickup();
                Destroy(hit.transform.gameObject);
            }
        }
    }
}

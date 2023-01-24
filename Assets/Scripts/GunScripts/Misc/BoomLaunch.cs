using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomLaunch : MonoBehaviour
{
    public float throwSpeed;
    float rotationSpeed;
    float speed;

    public GameObject bomb;
    public GameObject bombSpawn;

    public GameObject cam;

    public Rigidbody rb;

    //Refs
    public WeponManager WM;

    void FixedUpdate()
    {
        if (Input.GetKeyDown("f") && WM.boomNums >= 1 && WM.boomOut)
        {
            GameObject bombClone = Instantiate(bomb, bombSpawn.transform.position, Quaternion.identity);
            rb = bombClone.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward * 20 * throwSpeed, ForceMode.Force);
            WM.boomNums--;
            if(WM.boomNums <= 0)
            {
                WM.boomGFX.SetActive(false); 
            }
        }

        speed = rb.velocity.magnitude;
        rotationSpeed = rb.angularVelocity.magnitude * Mathf.Rad2Deg;
        rb.angularVelocity = new Vector3(Mathf.PI * 2, 0, 0);
    }
}

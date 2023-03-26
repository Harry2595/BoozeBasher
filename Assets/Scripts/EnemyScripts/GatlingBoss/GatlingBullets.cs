using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBullets : MonoBehaviour
{
    public float speed, bulletLife;

    public float pillarDamage;

    public Rigidbody myRigidBody;

    //Refs
    PillarHealth PHRef;

    // Update is called once per frame

    void Update()
    {
        BulletFly();

        bulletLife -= Time.deltaTime; //bulletLife = bulletLife - Time.deltaTime this destroys the bullet after a certain amount of time.

        if (bulletLife < 0)
        {
            Destroy(gameObject);
        }
    }

    private void BulletFly()
    {
        myRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pillar"))
        {
            PHRef = other.gameObject.GetComponent<PillarHealth>();
            PHRef.TakeDamage(pillarDamage);
        }
        Destroy(gameObject);
    }
}

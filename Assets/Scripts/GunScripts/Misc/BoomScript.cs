using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    private float explosionRadius = 5;
    private float explosionForce = 15;
    public float boomDamage;
    public ParticleSystem particle;
    public GameObject particleHolder;
    public GameObject GFX;

    //Refs
    BoomLaunch BLRef;

    void Start()
    {
        BLRef = BoomLaunch.instance.GetComponent<BoomLaunch>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != other.gameObject.CompareTag("Player")) {
            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var obj in surroundingObjects)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;

                if(obj != obj.CompareTag("Player") && obj != obj.CompareTag("Knifer") && obj != obj.CompareTag("PistolGuy"))
                {
                    rb.AddExplosionForce(explosionForce * 150, transform.position, explosionRadius);
                }
                else if(obj == obj.CompareTag("Knifer"))
                {
                    KnifeHitDetect Knifer = obj.GetComponent<KnifeHitDetect>();
                    Knifer.TakeDamage(boomDamage);
                    rb.AddExplosionForce(explosionForce * 150, transform.position, explosionRadius);
                }
                else if (obj == obj.CompareTag("PistolGuy"))
                {
                    PistolGuyHitDetect PistolGuy = obj.GetComponent<PistolGuyHitDetect>();
                    PistolGuy.TakeDamage(boomDamage);
                    rb.AddExplosionForce(explosionForce * 150, transform.position, explosionRadius);
                }
                else if (obj == obj.CompareTag("RepeaterGuy"))
                {
                    RepeaterGuyHitDetect RepeaterGuy = obj.GetComponent<RepeaterGuyHitDetect>();
                    RepeaterGuy.TakeDamage(boomDamage);                                                
                    rb.AddExplosionForce(explosionForce * 150, transform.position, explosionRadius); 
                }
                else
                {
                    rb.AddExplosionForce(explosionForce * 15, transform.position, explosionRadius);
                }
            }

            Rigidbody thisRB = gameObject.GetComponent<Rigidbody>();
            thisRB.freezeRotation = true;
            thisRB.constraints = RigidbodyConstraints.FreezePosition;

            particleHolder.SetActive(true);
            particle.Play();
            GFX.SetActive(false);

            Destroy(gameObject, 0.5f);
            BLRef.AssignRB();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

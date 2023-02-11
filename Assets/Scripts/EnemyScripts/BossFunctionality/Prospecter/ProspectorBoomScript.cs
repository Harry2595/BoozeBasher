using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProspectorBoomScript : MonoBehaviour
{
    private float explosionRadius = 5;
    private float explosionForce = 15;
    public float boomDamage;
    public ParticleSystem particle;
    public GameObject particleHolder;
    public GameObject GFX;

    //Refs
    ProspecterFunctionality PFRef;

    void Start()
    {
        PFRef = ProspecterFunctionality.instance.GetComponent<ProspecterFunctionality>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != other.gameObject.CompareTag("Prospector"))
        {
            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var obj in surroundingObjects)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;

                if(obj == obj.CompareTag("Player"))
                {
                    rb.AddExplosionForce(explosionForce * 100, transform.position, explosionRadius);
                    //DamagePlayer
                }
            }

            Rigidbody thisRB = gameObject.GetComponent<Rigidbody>();
            thisRB.freezeRotation = true;
            thisRB.constraints = RigidbodyConstraints.FreezePosition;

            particleHolder.SetActive(true);
            particle.Play();
            GFX.SetActive(false);

            Destroy(gameObject, 0.5f);
            PFRef.AssignRB();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

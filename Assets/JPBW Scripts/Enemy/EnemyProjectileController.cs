using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    Rigidbody myRigidBody;
    public float upForce, forwardForce;

    public int damageAmount = 3;

    LevelManager LMRef;  //references the levelManager

    // Start is called before the first frame update
    void Start()
    {
        LMRef = FindObjectOfType<LevelManager>(); //sets LMRef = to the levelManager in the scene
        damageAmount += LMRef.damageGain; //applies the damageGain to the damage of the projectile

        myRigidBody = GetComponent<Rigidbody>();

        GrenadeThrow();
    }

    private void GrenadeThrow()
    {
        myRigidBody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        myRigidBody.AddForce(transform.up * upForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); //I added this so it would not fire through walls
        }
    }
}

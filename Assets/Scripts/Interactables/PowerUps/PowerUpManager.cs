using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public int MaxHPIncrease;
    public int DamageIncrease;

    public PlayerHealthSystem PHSRef;
    public GunSystem GSRefRepeater;
    public GunSystem GSRefPistol;
    public GunSystem GSRefShotgun;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MaxHPPowerUp"))
        {
            PHSRef.PowerUpMaxHP(MaxHPIncrease);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("DmgPowerUp"))
        {
            GSRefRepeater.ExtraDamage(DamageIncrease);
            GSRefPistol.ExtraDamage(DamageIncrease);
            GSRefShotgun.ExtraDamage(DamageIncrease);
            Destroy(other.gameObject);
        }
    }
}

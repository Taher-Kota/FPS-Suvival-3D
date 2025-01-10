using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{    
    public int damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            other.GetComponent<Health>().ApplyDamage(WeaponManager.instance.GetCurrentWeapon().Damage);
        }
        else if(other.tag == Tags.Player)
        {
            other.GetComponent<Health>().ApplyDamage(damage);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    Animator anim;
    WeaponShoot weaponShoot;
    //public GameObject AttackPoint;
    private void Awake()
    {
        weaponShoot = transform.root.GetComponent<WeaponShoot>();
        anim = GetComponent<Animator>();
    }

    public void AttackAnim()
    {
        anim.SetTrigger(Tags.Attack);
    }

    public void Aim(bool CanAim)
    {
        anim.SetBool(Tags.Aim, CanAim);
    }

    //WeaponShootFunction
    public void AttackArrowSpear()
    {
       weaponShoot.CanAttackArrowSpear = true;
    }

    ////AttackPointFuntion
    //public void Activate()
    //{               
    //    AttackPoint.SetActive(true);
    //}
    //public void DeActivate()
    //{
    //    if (AttackPoint.activeInHierarchy)
    //    {
    //        AttackPoint.SetActive(false);
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponShoot : MonoBehaviour
{
    WeaponManager weaponManager;
    private float NextTimeToFire;
    public float Bulletspeed;
    bool CanAim;

    private GameObject CrossHair;
    private Animator FP_Camera_Anim;
    public GameObject Arrow_prefab, Spear_prefab,Bullet_Prefab;
    public Transform shootingPoint,BulletShootPoint;
    public bool CanAttackArrowSpear;
    private Camera maincam;
    float timerForotherWeapon;
    public ParticleSystem AssaultMuzzelFlash;
    public ParticleSystem RevolverMuzzelFlash;
    public ParticleSystem ShotgunMuzzelFlash;
    private SmartPool pool;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        FP_Camera_Anim = transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        CrossHair = GameObject.Find(Tags.CrossHair);
        CanAttackArrowSpear = true;
        maincam = Camera.main;
        pool = GameObject.Find("SmartPool").GetComponent<SmartPool>();
    }

    void Update()
    {
        Shoot();
        ZoomInAndOut();
    }
    void Shoot()
    {
        WeaponConfig CurrentWeapn = weaponManager.GetCurrentWeapon();
        FireType fireType = CurrentWeapn.fireType;
        AmmoType ammoType = CurrentWeapn.ammoType;
        if (fireType == FireType.Multiple)
        {
            if (Input.GetMouseButton(0) && Time.time > NextTimeToFire)
            {
                NextTimeToFire = Time.time + CurrentWeapn.FireRate;
                CurrentWeapn.anim.AttackAnim();
                BulletShoot();
                SoundManager.instance.AssaultSound();
                AssaultMuzzelFlash.Play();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > timerForotherWeapon)
            {
                timerForotherWeapon = Time.time + 1f;
                if (ammoType == AmmoType.None)
                {
                    CurrentWeapn.anim.AttackAnim();
                    SoundManager.instance.AxeSound();
                }
                else if (ammoType == AmmoType.Bullet)
                {
                    CurrentWeapn.anim.AttackAnim();
                    BulletShoot();
                    if (CurrentWeapn.name == "Revolver")
                    {
                        RevolverMuzzelFlash.Play();
                        SoundManager.instance.RevolverSound();                        
                    }
                    else
                    {
                        ShotgunMuzzelFlash.Play();
                        SoundManager.instance.ShotGunSound();
                    }
                }
                else if (ammoType == AmmoType.Spear && CanAim && CanAttackArrowSpear)
                {
                    CanAttackArrowSpear = false;
                    CurrentWeapn.anim.AttackAnim();
                    ArrowSpearShoot(Spear_prefab, 15f);
                }
                else if (CanAim && CanAttackArrowSpear)
                {
                    CanAttackArrowSpear = false;
                    CurrentWeapn.anim.AttackAnim();
                    ArrowSpearShoot(Arrow_prefab, 20f);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1) && (ammoType == AmmoType.Spear || ammoType == AmmoType.Bow))
                {
                    CanAim = true;
                    CurrentWeapn.anim.Aim(CanAim);
                }
                else if (Input.GetMouseButtonUp(1) && (ammoType == AmmoType.Spear || ammoType == AmmoType.Bow))
                {
                    CanAim = false;
                    CurrentWeapn.anim.Aim(CanAim);
                }
            }
        }
    }

    void ZoomInAndOut()
    {
        WeaponConfig CurrentWeapon = weaponManager.GetCurrentWeapon();

        if (CurrentWeapon.ammoType == AmmoType.Bullet)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CrossHair.SetActive(false);
                switch (CurrentWeapon.weaponName)
                {
                    case WeaponName.Assault_Rifle:
                        FP_Camera_Anim.Play(Tags.AssualtZoom);
                        break;
                    case WeaponName.Shotgun:
                        FP_Camera_Anim.Play(Tags.ShotGunZoom);
                        break;
                    case WeaponName.Revolver:
                        FP_Camera_Anim.Play(Tags.RevolverZoom);
                        break;
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                CrossHair.SetActive(true);
                FP_Camera_Anim.Play(Tags.ZoomOut);
            }
        }
    }

    void ArrowSpearShoot(GameObject projectilePrefab, float force)
    {

        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce(shootingPoint.forward * force, ForceMode.VelocityChange);
    }

    void BulletShoot()
    {
        GameObject Bullet = pool.SpawnBullets(maincam.transform.position, BulletShootPoint.rotation);
        Bullet.GetComponent<Rigidbody>().velocity = maincam.transform.forward * Bulletspeed;
    }
}

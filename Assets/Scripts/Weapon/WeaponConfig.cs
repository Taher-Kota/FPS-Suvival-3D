
using UnityEngine;

public enum AimType
{
    None,
    Self_Aim,
    Aim
}

public enum AmmoType
{
    Bullet,
    Spear,
    Bow,
    None
}

public enum FireType
{
    Single,
    Multiple
}

public enum WeaponName
{
    Axe,
    Assault_Rifle,
    Revolver,
    Shotgun,
    Wooden_Spear,
    Wooden_Bow
}
public class WeaponConfig : MonoBehaviour
{
    public AimType aimType;
    public AmmoType ammoType;
    public FireType fireType;
    public WeaponName weaponName;
    public WeaponAnim anim;

    private void Awake()
    {
        anim = GetComponent<WeaponAnim>();
    }

    [Range(0,100)]
    public int Damage;

    [Range(0f,1f)]
    public float FireRate;

}

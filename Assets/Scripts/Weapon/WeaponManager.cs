
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponConfig[] Weapons;   
    private int currentWeaponIndex;
    private CharacterController ch;
    public static WeaponManager instance;
    SmartPool pool;
    public static int BulletCreated = 0;
    public static int BowCreated = 0;
    public static int ArrowCreated = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ch = GetComponent<CharacterController>();
        pool = GameObject.Find("SmartPool").GetComponent<SmartPool>();
    }
    void Start()
    {
        currentWeaponIndex = 0;
        Weapons[currentWeaponIndex].gameObject.SetActive(true);
        pool.CreateBullets(20);
    }

    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeWeapon(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeWeapon(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ChangeWeapon(5);
            }
        }
    }

    void ChangeWeapon(int index)
    {
        if (currentWeaponIndex == index) return;
        if (!ch.isGrounded) return;
        Weapons[currentWeaponIndex].gameObject.SetActive(false);
        Weapons[index].gameObject.SetActive(true);
        currentWeaponIndex = index;
    }

    public WeaponConfig GetCurrentWeapon()
    {
        return Weapons[currentWeaponIndex];
    }
}

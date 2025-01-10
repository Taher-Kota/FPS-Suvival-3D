using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    public GameObject Bullet;
    private List<GameObject> BulletList = new List<GameObject>();

    public void CreateBullets(int amount)
    {
        for (int i = 0; i <= amount; i++)
        {
            GameObject tempBullets = Instantiate(Bullet);
            tempBullets.SetActive(false);
            BulletList.Add(tempBullets);
        }
    }

    public GameObject SpawnBullets(Vector3 pos, Quaternion rotation)
    {
        for (int i = 0; i <= BulletList.Count; i++)
        {              
            if (!BulletList[i].activeInHierarchy)
            {
                BulletList[i].SetActive(true);
                BulletList[i].transform.position = pos;
                BulletList[i].transform.rotation = rotation;
                return BulletList[i];
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    float XaxixMin = 10f, XaxixMax = 200f;
    float ZaxixMin = 20f, ZaxixMax = 200f;    
    public GameObject CannibalPrefab, BoarPrefab;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, 10f);
    }

    void SpawnEnemies()
    {
        float Xaxix = Random.Range(XaxixMin, XaxixMax);
        float Zaxix = Random.Range(ZaxixMin, ZaxixMax);       
        GameObject Enemy = Random.value > .5f ? CannibalPrefab : BoarPrefab;
        Vector3 pos = new Vector3(Xaxix, Enemy.transform.position.y, Zaxix);
        NavMesh.SamplePosition(pos, out NavMeshHit hit, 100f,-1);
        Instantiate(Enemy, hit.position, Quaternion.identity);
    }
}

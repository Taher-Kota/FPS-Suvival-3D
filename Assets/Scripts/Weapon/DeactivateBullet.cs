using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeactivateBullet : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Deactivate",1.5f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

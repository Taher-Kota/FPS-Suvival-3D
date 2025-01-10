using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public bool is_Player, is_Cannibal, is_Boar;
    private bool is_Dead;
    private float health = 100f;
    private EnemyController enemyControl;
    private EnemyAnim enemyAnim;
    private PlayerStats playerStats;
    GameObject FP_Camera;
    MouseInput mouseInput;

    private void Awake()
    {
        if (is_Boar || is_Cannibal)
        {
            enemyControl = GetComponent<EnemyController>();
            enemyAnim = GetComponent<EnemyAnim>();
        }
        if (is_Player)
        {
            mouseInput = transform.GetChild(0).GetComponent<MouseInput>();
            FP_Camera = GameObject.Find("FP Camera");
            playerStats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if(is_Dead) { return; }
        health -= damage;

        if (is_Player)
        {
            playerStats.DisplayHealth(health);
        }
        if(is_Boar || is_Cannibal)
        {
            if(enemyControl.Enemystate == EnemyState.PATROL)
            {
                enemyControl.ChaseDistance = 100f;
            }
        }

        if(health <= 0)
        {
            Died();
            is_Dead = true;
        }
    }

    void Died()
    {
        if (is_Cannibal)
        {
            StartCoroutine("Deactivate");
        }
        if (is_Boar)
        {
            SoundManager.instance.BoarDeadSound();
            enemyControl.enabled = false;
            enemyAnim.Dead();
            StartCoroutine("Deactivate");
        }
        if (is_Player)
        {
            mouseInput.enabled = false;
            FP_Camera.SetActive(false);
            GameObject[] enemis = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemis)
            {
                enemy.SetActive(false);
            }
            StartCoroutine("Deactivate");
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3f);
        if (is_Player)
        {
            GetComponent<PlayerController>().enabled = false;
            SceneManager.LoadScene(0);
        }
        else gameObject.SetActive(false);
    }
}

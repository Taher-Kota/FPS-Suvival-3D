using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        anim.SetBool(Tags.CannibalWalk, walk);
    }
    
    public void Run(bool run)
    {
        anim.SetBool(Tags.CannibalRun, run);
    }

    public void Attack()
    {
        anim.SetTrigger(Tags.Attack);
    }
    public void Dead()
    {
        anim.SetTrigger(Tags.BoarDead);
    }
}

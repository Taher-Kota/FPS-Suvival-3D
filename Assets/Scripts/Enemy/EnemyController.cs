using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyState enemyState;
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField]
    private float Current_Patrol_Time, Max_Patrol_Time = 15f;
    [SerializeField]
    private float Walk_Speed = .5f, Run_Speed = 4f;
    [SerializeField]
    private float AttackTime, Wait_Befor_Attack = 2f;
    [SerializeField]
    private float MinRadius = 20f, MaxRadius = 60f;
    [SerializeField]
    public float ChaseDistance = 7f;
    private float CurrentChaseDist, ChaseAfterDistance = 2f, AttackDistance = 2f;
    EnemyAnim anim;
    private void Awake()
    {
        target = GameObject.Find(Tags.Player).transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<EnemyAnim>();
        enemyState = EnemyState.PATROL;
    }
    void Start()
    {
        Current_Patrol_Time = Max_Patrol_Time;
        CurrentChaseDist = ChaseDistance;        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();

            if (Vector3.Distance(transform.position, target.transform.position) <= ChaseDistance)
            {
                anim.Walk(false);
                enemyState = EnemyState.CHASE;
                SoundManager.instance.CannibalScreemSound();
            }           
        }
        if (enemyState == EnemyState.CHASE)
        {
            Chase();
            if (Vector3.Distance(transform.position, target.transform.position) > ChaseDistance)
            {
                anim.Run(false);
                enemyState = EnemyState.PATROL;
            }
        }
        if (enemyState == EnemyState.ATTACK)
        {
            Attack();            
        }
    }

    void Patrol()
    {
        //agent.isStopped = false;
        agent.speed = Walk_Speed;
        Current_Patrol_Time += Time.deltaTime;
        if (Current_Patrol_Time >= Max_Patrol_Time)
        {
            Max_Patrol_Time = Random.Range(5f, 15f);
            Current_Patrol_Time = 0;
            SetPatrolDestination();
        }
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Walk(true);
        }
        else
        {
            anim.Walk(false);
        }
    }

    void Chase()
    {    
       // agent.isStopped = false;
        agent.speed = Run_Speed;
        agent.SetDestination(target.position);
        if (agent.velocity.sqrMagnitude > 0)
        {
            anim.Run(true);
        }
        else
        {
            anim.Run(false);
        }

        if(Vector3.Distance(transform.position,target.position) <= AttackDistance)
        {
            anim.Run(false);
            anim.Walk(false);
            enemyState = EnemyState.ATTACK;
        }
        if (agent.velocity.sqrMagnitude <= 0)
        {
            if (ChaseDistance != CurrentChaseDist)
            {
                ChaseDistance = CurrentChaseDist;
            }
        }
    }

    void Attack()
    {
        //agent.isStopped = true;
        AttackTime += Time.deltaTime;
        if(AttackTime > Wait_Befor_Attack)
        {
            AttackTime = 0f;
            anim.Attack();
            transform.LookAt(target);
            if (this.gameObject.name == "Cannibal")
            {
                SoundManager.instance.CannibalAttackSound();
            }
            else SoundManager.instance.BoarAttackSound();
        }
        if(Vector3.Distance(transform.position, target.position) >= AttackDistance + ChaseAfterDistance && 
            Vector3.Distance(transform.position, target.position) <= ChaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    void SetPatrolDestination()
    {
        float random_radius = Random.Range(MinRadius, MaxRadius);

        Vector3 pos = Random.insideUnitSphere * random_radius;
        pos += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(pos, out hit, random_radius, -1);
        agent.SetDestination(hit.position);
    }

    public EnemyState Enemystate { get; private set; }
}

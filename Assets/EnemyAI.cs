using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chasingRadious = 6f;
    [SerializeField]GameObject enemy;
    NavMeshAgent agent;
    public float targetDistance = Mathf.Infinity;
    public bool isProved = false;
    public bool isAttack = false;
    public string idleAni;
    public string runAni;
    public string attackAni;
    public float fireR = 15f;
    private float nTTF = 0f;
    [SerializeField] float damage = 10f;  
    PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = target.transform.GetComponent<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {

        targetDistance = Vector3.Distance(target.position, transform.position);

        Target();
        if (targetDistance < chasingRadious&& !isAttack) 
        {
            isProved = true;
        }
        else if (targetDistance >=  chasingRadious)
        {
            isProved = false;
            agent.SetDestination(this.transform.position);
            StartCoroutine(Idle());
        }
        if (isProved == true)
        {
            DelayWithTarget();
        }
    }

    private void DelayWithTarget()
    {
        if (targetDistance > agent.stoppingDistance)
        {
            ChaseTarget();

        }
        else if (targetDistance > agent.stoppingDistance)
        {
            agent.SetDestination(this.transform.position);
        }
        
    }
    void Target()
    {
        if (targetDistance <= agent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    void AttackTarget()
    {
        if (targetDistance <= agent.stoppingDistance)
        {
            isAttack = true;
            StartCoroutine(Attack());
            
        }
        

    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
        if (!isAttack)
        {
            StartCoroutine(Run());
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasingRadious);
    }
    IEnumerator Attack()
    {
        enemy.GetComponent<Animator>().Play(attackAni);
        yield return new WaitForSeconds(1);
        isAttack = false;
    }
    IEnumerator Run()
    {
        enemy.GetComponent<Animator>().Play(runAni);
        yield return new WaitForSeconds(1);
    }
    IEnumerator Idle()
    {
        enemy.GetComponent<Animator>().Play(idleAni);
        yield return new WaitForSeconds(1);
    }


}

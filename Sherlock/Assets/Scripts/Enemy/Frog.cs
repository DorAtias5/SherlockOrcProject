using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : PatrolLog
{
    public float attackCooldown;
    public bool canAttack;
    private float attackCooldownSec;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        canAttack = true;
        attackCooldownSec = attackCooldown;
    }
    public override void CheckDist()
    {
        if (Vector3.Distance(target.position, transform.position) <= cheseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRb.MovePosition(temp);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > cheseRadius)
        {
            if (Vector3.Distance(transform.position, path[currnetPoint].position) > distanceDeviant)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currnetPoint].position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRb.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }

        else if(Vector3.Distance(target.position, transform.position) <= cheseRadius 
            && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (canAttack)
            {
                Debug.Log("can att");
                if (currentState == EnemyState.walk && currentState != EnemyState.stagger) 
                { 
                    StartCoroutine(attackCo());
                    canAttack = false;
                }
            }
        }
    }


    public IEnumerator attackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackCooldown);
        currentState = EnemyState.walk;
        anim.SetBool("isAttacking",false);
    }
    private void Update()
    {
        attackCooldownSec -= Time.deltaTime;
        if (attackCooldownSec <= 0)
        {
            canAttack = true;
            attackCooldownSec = attackCooldown;
        }
    }
}

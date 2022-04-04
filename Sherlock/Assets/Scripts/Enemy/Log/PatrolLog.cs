using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currnetPoint;
    public Transform currentGoal;
    public float distanceDeviant;

    public override void OnEnable()
    {
        base.OnEnable();
        currentState = EnemyState.walk;
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
                anim.SetBool("isAwake", true);

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
    }

    public void ChangeGoal()
    {
        if(currnetPoint == path.Length - 1)
        {
            currnetPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currnetPoint++;
            currentGoal = path[currnetPoint];
        }
    }
}

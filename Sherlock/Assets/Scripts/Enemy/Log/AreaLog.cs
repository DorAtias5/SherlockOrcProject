using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    public Collider2D areaBounds;

    public override void CheckDist()
    {
        if (Vector3.Distance(target.position, transform.position) <= cheseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && areaBounds.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                myRb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("isAwake", true);

            }
        }
        else if (Vector3.Distance(target.position, transform.position) > cheseRadius
            || !areaBounds.bounds.Contains(target.transform.position))
        {
            anim.SetBool("isAwake", false);
        }
    }
}

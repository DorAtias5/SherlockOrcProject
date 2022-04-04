using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossHp : EnemyHealth
{
    public TreeBoss theBoss;
    public override void TakeDmg(float amount)
    {
        //play effect
        GameObject tempEffect = Instantiate(gotHitEffect, transform.position, Quaternion.identity);
        currHp -= amount;
        
        if(currHp <= 25 && currHp > 20)
        {
            //stage 1 water
            theBoss.StageTransform(1);
        }
        else if (currHp <=20 && currHp > 10) 
        {
            //stage 2 bombs
            theBoss.StageTransform(2);
        }
        else
        {
            //both!
            theBoss.StageTransform(3);

        }
        if (currHp <= 0)
        {
            currHp = 0;
            Destroy(tempEffect, 1f);
            Die();
        }
        Destroy(tempEffect, 1f);
    }

    public override void Die()
    {
        this.gameObject.GetComponentInParent<Enemy>().playDeathEffect();
        this.gameObject.GetComponentInParent<Enemy>().MakeItRain();
        theBoss.gameObject.SetActive(false);
    }
}

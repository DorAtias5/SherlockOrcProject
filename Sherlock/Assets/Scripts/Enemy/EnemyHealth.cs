using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    public GameObject gotHitEffect;
    private void OnEnable()
    {
        currHp = maxHp.runTimeValue;
    }
    public override void Die()
    {
        this.gameObject.GetComponent<Enemy>().playDeathEffect();
        this.gameObject.GetComponent<Enemy>().MakeItRain();
        this.gameObject.SetActive(false);
    }

    public override void TakeDmg(float amount)
    {
        //play effect
        GameObject tempEffect = Instantiate(gotHitEffect, transform.position, Quaternion.identity);
        currHp -= amount;
        if (currHp <= 0)
        {
            currHp = 0;
            Destroy(tempEffect, 1f);
            Die();
        }
        Destroy(tempEffect, 1f);
    }

}

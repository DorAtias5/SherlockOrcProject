using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPoweUp : PowerUp
{
    public float hpToIncrease;
    public FloatValue hpContainers;
    public FloatValue playerHp;


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    IncreaceHp(hpToIncrease);
                    powerupSignal.Rise();
                    activeObj = false;
                    Destroy(this.gameObject);
                }
                playerInRange = true;
            }
        }
    }

    public void IncreaceHp(float hpToIncrease)
    {
        playerHp.runTimeValue += hpToIncrease;
        if(playerHp.runTimeValue > hpContainers.runTimeValue * 2) //overheal
        {
            playerHp.runTimeValue = hpContainers.runTimeValue * 2;
        }
    }
}

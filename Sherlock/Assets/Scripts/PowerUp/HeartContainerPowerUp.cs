using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerPowerUp : PowerUp
{
    public FloatValue hpContainers;
    public FloatValue playerHp;
    public Signal addHpContainer;
    //heart container power 
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    IncreaceContainer();
                    addHpContainer.Rise();
                    powerupSignal.Rise();
                    activeObj = false;
                    Destroy(this.gameObject);
                }
                playerInRange = true;
            }
        }
    }

    public void IncreaceContainer()
    {
        hpContainers.runTimeValue++; //get one container
        playerHp.runTimeValue += 2; //heal 1 heart
        if (playerHp.runTimeValue > hpContainers.runTimeValue * 2) //overheal
        {
            playerHp.runTimeValue = hpContainers.runTimeValue * 2;
        }
    }
}

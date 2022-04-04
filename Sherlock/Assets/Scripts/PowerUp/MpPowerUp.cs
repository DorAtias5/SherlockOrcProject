using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpPowerUp : PowerUp
{
    public float mpToIncrease;
    public FloatValue mpContainers;
    public FloatValue playerMp;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    IncreaceMp(mpToIncrease);
                    powerupSignal.Rise();
                    activeObj = false;
                    Destroy(this.gameObject);
                }
                playerInRange = true;
            }
        }
    }

    public void IncreaceMp(float hpToIncrease)
    {
        playerMp.runTimeValue += mpToIncrease;
        if (playerMp.runTimeValue > mpContainers.runTimeValue * 3) 
        {
            playerMp.runTimeValue = mpContainers.runTimeValue * 3;
        }
    }
}

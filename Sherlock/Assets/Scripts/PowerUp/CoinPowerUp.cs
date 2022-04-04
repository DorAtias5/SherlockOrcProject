using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : PowerUp
{
    public int coinValue;
    public PlayerInventory playerInv;


    public void AddCoins(int coinValue)
    {
        playerInv.coins += coinValue; //mybe protect this with a limit
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    AddCoins(coinValue);
                    powerupSignal.Rise();
                    activeObj = false;
                    Destroy(this.gameObject);
                }
                playerInRange = true;
            }
        }
    }
}

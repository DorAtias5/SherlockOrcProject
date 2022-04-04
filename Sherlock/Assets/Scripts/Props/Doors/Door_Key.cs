using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Key : Door
{
    public PlayerInventory playerInv;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                if (playerInv.numOfKeys > 0)
                {
                    clueSignal.Rise();
                    playerInv.numOfKeys--;
                    OpenDoor();
                    activeObj = false;
                }
            }
            else
            {
               
            }
        }
    }

}

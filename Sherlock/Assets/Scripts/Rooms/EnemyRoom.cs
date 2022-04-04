using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : DungeonRoom
{
    public Door[] doorsToInteract;
    
    public void CheckRoomClear() //itrate enemies array to see if all have been defeated
    {
        if (EnemiesActive() == 1)
        {
            OpenAllDoors();
        }
    }
    public int EnemiesActive()
    {
        int activeEnemies = 0;
        for (int i = 0; i < enemiesArr.Length; i++)
        {
            if (enemiesArr[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //enable arrays
            for (int i = 0; i < enemiesArr.Length; i++)
            {
                ChangeActive(enemiesArr[i], true);
            }
            for (int i = 0; i < potsArr.Length; i++)
            {
                ChangeActive(potsArr[i], true);
            }
            CloseAllDoors();
            virtualCam.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //dissable arrays
            for (int i = 0; i < enemiesArr.Length; i++)
            {
                ChangeActive(enemiesArr[i], false);
            }
            for (int i = 0; i < potsArr.Length; i++)
            {
                ChangeActive(potsArr[i], false);
            }
            virtualCam.SetActive(false);
        }
    }


    public void OpenAllDoors()
    {
        for(int i = 0; i<doorsToInteract.Length; i++)
        {
            doorsToInteract[i].OpenDoor();
        }

    }

    public void CloseAllDoors()
    {
        for (int i = 0; i < doorsToInteract.Length; i++)
        {
            doorsToInteract[i].CloseDoor();
        }

    }
}

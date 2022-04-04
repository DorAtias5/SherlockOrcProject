using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItems : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInv;
    [SerializeField] private InventoryItem thisItem;


    void AddToInv()
    {
        if (thisItem && playerInv)
        {
            if (playerInv.playerInv.Contains(thisItem))
            {
                thisItem.numberInInv++;
            }
            else
            {
                playerInv.playerInv.Add(thisItem);
                thisItem.numberInInv++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddToInv();
            Destroy(this.gameObject);
        }
    }

}

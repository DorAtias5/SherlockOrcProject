using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the actual object containing the player inventory
[System.Serializable]
[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory/player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public InventoryItem currentItem;
    public int numOfKeys;
    public int coins;
    public List<InventoryItem> playerInv = new List<InventoryItem>();

    public void Awake()
    {
        coins = 0;
    }

    public void AddToInv(InventoryItem itemToAdd) //for chests
    {
        //is it key?
        if (itemToAdd.isKey)
        {
            numOfKeys++;
        }
        else // add to inventory if we dont already have one
        {
            playerInv.Add(itemToAdd);
        }
    }
}

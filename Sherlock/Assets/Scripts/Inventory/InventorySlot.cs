using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the prefab script for an inventory slot that containt an inventory item
//will update the inventory ui to show slots "inventory item" inside
public class InventorySlot : MonoBehaviour
{
    [Header("Item Slot in the Ui")]
    [SerializeField] private Text itemCounterText;
    [SerializeField] private Image itemImg;

    [Header("Vars")]
    public InventoryItem thisItem;
    public InventoryManager invManager;

    public void SetUp(InventoryItem item, InventoryManager manager)
    {
        thisItem = item;
        invManager = manager;
        if (thisItem)
        {
            itemImg.sprite = thisItem.itemImg; //show correct img
            if (!thisItem.unique)
            {
                itemCounterText.text = "" + thisItem.numberInInv; // show correct qunatity
            }
            else
            {
                itemCounterText.text = "";
            }
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            invManager.SetItemInfo(thisItem.description, thisItem.usable, thisItem);
        }
    }
}

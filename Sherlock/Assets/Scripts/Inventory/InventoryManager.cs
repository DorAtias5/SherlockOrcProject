using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//create inventory slots and show them on the ui with the correct discription and ues button
public class InventoryManager : MonoBehaviour
{
    public PlayerInventory playerInv;

    [SerializeField] private GameObject blankInvSlot;
    [SerializeField] private GameObject invPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject uesBtn;
    public FloatValue playerHp;
    public FloatValue playerMp;
    public FloatValue playerHpContainer;
    public FloatValue playerMpContainer;
    public InventoryItem currentItem;

    public void SetTxtNBtn(string descrip, bool btnActive)
    {
        descriptionText.text = descrip;
        if (btnActive)
        {
            uesBtn.SetActive(true);
        }
        else
        {
            uesBtn.SetActive(false);
        }
    }

    void OnEnable()
    {
        CleanUpInv();
        CreateInvSlots();
        SetTxtNBtn("", false);
    }

    void CreateInvSlots()
    {
        if (playerInv)
        {
            for(int i = 0; i < playerInv.playerInv.Count; i++)
            {
                if (playerInv.playerInv[i].numberInInv > 0)
                {
                    GameObject temp = Instantiate(blankInvSlot, invPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(invPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.SetUp(playerInv.playerInv[i], this);
                    }
                }
            }
        }
    }

    public void SetItemInfo(string description, bool isUesable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = description;
        uesBtn.SetActive(isUesable);
    }
    
    private void CleanUpInv()
    {
        for(int i = 0; i< invPanel.transform.childCount; i++)
        {
            Destroy(invPanel.transform.GetChild(i).gameObject); //clear the pannel 
        }
    }

    public void UesBtn()
    {
       InventoryItem.TypeOfPotion thisType= currentItem.thisType;
        switch (thisType)
        {
            case InventoryItem.TypeOfPotion.hp:
                if (playerHp.runTimeValue < playerHpContainer.runTimeValue * 2)
                {
                    UesAndCreate();
                }
                else
                {
                    Debug.Log("already maxed");
                }
                break;

            case InventoryItem.TypeOfPotion.mp:
                if (playerMp.runTimeValue < playerMpContainer.runTimeValue * 3)
                {
                    UesAndCreate();
                }
                else
                {
                    Debug.Log("already maxed");
                }
                break;

            default:
                currentItem.Use();
                break;
        }

    }
    public void UesAndCreate()
    {
        currentItem.Use();
        //clear inv slots so we can update the inventory
        CleanUpInv();
        //refill slots with whats left 
        CreateInvSlots();
        if (currentItem.numberInInv == 0)
        {
            SetTxtNBtn("", false);
        }
    }
}

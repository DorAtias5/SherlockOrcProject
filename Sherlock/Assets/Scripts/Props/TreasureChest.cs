using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable
{
    [Header("Chest Content")]
    public PlayerInventory playerInv;
    public InventoryItem contents;
    public bool isOpen;
    public BooleanValue storeOpenStatue;

    [Header("Helpers")]
    public Signal getItemSignal;
    public GameObject textBubble;
    public TextMeshProUGUI myTxt;

    private Animator chestAnim;
    

    void Start()
    {
        chestAnim = GetComponent<Animator>();
        isOpen = false;
        activeObj = true;
        isOpen = storeOpenStatue.runTimeValue;
        if (isOpen)
        {
            chestAnim.SetBool("isOpen", true);
        }
    }


    void Update()
    {
            if (Input.GetButtonDown("check") && playerInRange)
            {
                if (!isOpen)
                {
                    // open the chest  + animation
                    OpenChest();
                    //give item
                }
                else
                {
                    IsAlreadyOpen();
                }
            }
    }

    public void OpenChest()
    {
        //open dialog box and say somthing
        textBubble.SetActive(true);
        myTxt.text = contents.relatedText;
        //add item to player inventory
        playerInv.AddToInv(contents);
        playerInv.currentItem = contents;
        //play holding item anim
        getItemSignal.Rise();
        //set chest to open state
        isOpen = true;
        //context clue off
        clueSignal.Rise();
        activeObj = false;
        chestAnim.SetBool("isOpen", true);
        storeOpenStatue.runTimeValue = isOpen;
    }
    
    public void IsAlreadyOpen()
    {
            //dialog off 
            textBubble.SetActive(false);
            //rise signal to stop animating player
            getItemSignal.Rise();
            activeObj = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            if (!playerInRange)
            {
                if (activeObj)
                {
                    clueSignal.Rise();
                }
                playerInRange = true;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            if (playerInRange)
            {
                if (activeObj)
                {
                    clueSignal.Rise();
                }
                playerInRange = false;
            }
        }
    }


}

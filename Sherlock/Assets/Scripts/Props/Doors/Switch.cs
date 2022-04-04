using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool isActive;
    public BooleanValue stordeVal;
    public GameObject textBubble;
    public Text dialogText;
    public Door partnerDoor;
    public string activeTriggerComment;
    public bool playerInRange;
    void Start()
    {
        playerInRange = false;
        isActive = stordeVal.runTimeValue;
        if (isActive)
        {
            ActivateSwitch();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            ActivateSwitch();
            //say somthing
            if (textBubble != null && dialogText != null)
            {
                textBubble.SetActive(true);
                dialogText.text = activeTriggerComment;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
        if (textBubble != null && dialogText != null)
        {
            textBubble.SetActive(false);
        }
    }

    public void ActivateSwitch()
    {
        //open door
        isActive = true;
        stordeVal.runTimeValue = isActive;
        partnerDoor.OpenDoor();
    }
}

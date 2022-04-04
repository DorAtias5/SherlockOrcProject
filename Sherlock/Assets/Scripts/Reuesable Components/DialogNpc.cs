using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNpc : Interactable
{
    [SerializeField] private TextAssetValue dialogVal;
    //reff to the npc dialog
    [SerializeField] private TextAsset dialogAsset;
    //notification to send to the canvases to activate and check

    //dialog
    [SerializeField] private Signal branchingDialogSignal;
    [SerializeField] private Signal CloseDialogNpc;

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("check"))
            {
                dialogVal.Value = dialogAsset;
                branchingDialogSignal.Rise();
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    clueSignal.Rise();
                    CloseDialogNpc.Rise();
                }
                playerInRange = false;
            }
        }
    }
}

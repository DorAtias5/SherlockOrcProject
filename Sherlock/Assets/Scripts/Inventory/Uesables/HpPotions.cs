using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotions : MonoBehaviour
{
    public FloatValue playerHp;
    public FloatValue heartContainer;
    public Signal hpSignal;
    public InventoryItem thisItem;
    // Start is called before the first frame update
    
    public void Ues(int hpToIncrease)
    {
            playerHp.runTimeValue += hpToIncrease;
            if (playerHp.runTimeValue > heartContainer.runTimeValue * 2) //overheal
            {
                playerHp.runTimeValue = heartContainer.runTimeValue * 2;
            }
            hpSignal.Rise();
    }
}

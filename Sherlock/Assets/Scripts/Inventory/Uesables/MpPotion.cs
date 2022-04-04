using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpPotion : MonoBehaviour
{
    public FloatValue playerMp;
    public FloatValue mpContainer;
    public Signal mpSignal;

    public void Ues(int mpToIncrease)
    {
        playerMp.runTimeValue += mpToIncrease;
        if (playerMp.runTimeValue > mpContainer.runTimeValue * 3) //overheal
        {
            playerMp.runTimeValue = mpContainer.runTimeValue * 3;
        }
        mpSignal.Rise();
    }
}

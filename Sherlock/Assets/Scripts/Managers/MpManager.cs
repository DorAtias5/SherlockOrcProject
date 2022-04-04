using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpManager : HpManager
{
    public Sprite lastDrop;
    public FloatValue playerCurrentMp;
    public FloatValue MpContainers;

    public override void setUp()
    {
        for (int i = 0; i < MpContainers.initialValue; i++)
        {
            heartsArr[i].gameObject.SetActive(true);
            heartsArr[i].sprite = fullHeart;
        }
    }

    public override void UpdateHearts()
    {
        float tempMp = playerCurrentMp.runTimeValue / 3; // half a heart is 1 hp
        for (int i = 0; i < MpContainers.initialValue; i++)
        {
            if (i <= tempMp - 1)
            {
                heartsArr[i].sprite = fullHeart;
            }
            else if (i >= tempMp)
            {
                heartsArr[i].sprite = emptyHeart;
            }
            else if((playerCurrentMp.runTimeValue - 1) % 3 == 0)
            {
                heartsArr[i].sprite = lastDrop;
            }
            else
            {
                heartsArr[i].sprite = halfFullHeart;
            }
        }
    }
}
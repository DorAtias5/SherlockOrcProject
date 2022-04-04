using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LootTable : ScriptableObject 
{
    public Loot[] lootArr;
    public PowerUp RollForLoot()
    {
        int cumulativeProbs = 0;
        int currentProbs = Random.Range(0, 100);
        for (int i = 0; i < lootArr.Length; i++)
        {
            cumulativeProbs += lootArr[i].lootChance;
            if(currentProbs <= cumulativeProbs)
            {
                return lootArr[i].thisLoot;
            }
        }
        return null;
    }
    
}
[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public int lootChance; //in precentage
}

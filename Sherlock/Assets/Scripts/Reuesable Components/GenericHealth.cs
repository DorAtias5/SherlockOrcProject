using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHp;
    public float currHp;

    void Start()
    {
        currHp = maxHp.runTimeValue;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Heal(float amount)
    {
        currHp += amount;
        if(currHp > maxHp.initialValue)
        {
            currHp = maxHp.initialValue;
        }
    }

    public virtual void FullHeal()
    {
        currHp = maxHp.initialValue;
    }

    public virtual void TakeDmg(float amount)
    {
        currHp -= amount;
        if(currHp < 0)
        {
            currHp = 0;
        }
    }

    public virtual void Die()
    {
        currHp = 0;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BooleanValue : ScriptableObject
{
    public bool initialValue;

    public bool runTimeValue;
}

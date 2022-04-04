using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    //5.493517 -3.630581 celler start room
    // Start is called before the first frame update
    public Vector2 initialValue; //runtime
    public Vector2 defaultValue; // on start

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
    }
}

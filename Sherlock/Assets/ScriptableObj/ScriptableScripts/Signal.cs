using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{

    public List<SignalListener> listenersArr = new List<SignalListener>();

    public void Rise()
    {
        for(int i = listenersArr.Count - 1; i >= 0; i--) //itrate from the end so we can remve stuff without errors
        {
            listenersArr[i].OnSignalRise();
        }
    }

    //add and remove methods
    public void RegisterListner(SignalListener listener)
    {
        listenersArr.Add(listener);
    }

    public void DeRegisterListner(SignalListener listener)
    {
        listenersArr.Remove(listener);
    }

}

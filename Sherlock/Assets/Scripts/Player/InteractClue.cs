using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClue : MonoBehaviour
{
    public GameObject interactBox;
    public bool interactActive = false;
    

    public void changeContext()
    {
        interactActive = !interactActive;
        if (interactActive)
        {
            interactBox.SetActive(true);
        }
        else
        {
            interactBox.SetActive(false);
        }
    }
}

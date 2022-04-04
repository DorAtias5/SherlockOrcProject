using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //this class detect player and rise the question mark
    public bool playerInRange;
    public Signal clueSignal;
    public bool activeObj;
    // Start is called before the first frame update
    void Start()
    {
        activeObj = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    clueSignal.Rise();
                }
                playerInRange = true;
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                if (activeObj)
                {
                    clueSignal.Rise();
                }
                playerInRange = false;
            }
        }
    }
}

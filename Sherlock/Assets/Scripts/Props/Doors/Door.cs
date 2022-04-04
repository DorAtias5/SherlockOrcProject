using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : Interactable
{
    public Collider2D myRb;
    public bool isOpen;
    public Sprite openSprite;
    public Sprite closeSprite;
    public SpriteRenderer mySpr;
    // Start is called before the first frame update
    void Start()
    {
        activeObj = true;
        mySpr.sprite = closeSprite;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public virtual void  OpenDoor()
    {
        //change sprite
        //set door to open
        isOpen = true;
        //setrigidBody to inactive
        myRb.enabled = false;
        mySpr.sprite = openSprite;
    }


    public virtual void CloseDoor()
    {
        isOpen = false;
        myRb.enabled = true;
        mySpr.sprite = closeSprite;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NpcStates
{
    move,
    idle,
    interact
}

public class Npc_random : Sign
{
    public NpcStates currentState;
    public float timeBetweenMoves;
    public float speed;
    public Transform myTrans;
    public Rigidbody2D myRb;
    public Animator myAnim;
    private Vector3 dirVec;
    public Collider2D bound;
    private float timeBetweenSec;
    void Start()
    {
        currentState = NpcStates.move;
        timeBetweenSec = timeBetweenMoves;
        activeObj = true;
        myTrans = GetComponent<Transform>();
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        ChangeDir();
        myAnim.SetBool("isMoving",true);
    }

   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerInRange && currentState == NpcStates.move)
        {
            Move();
        }
        if (timeBetweenSec <= 0 && currentState != NpcStates.interact)
        {
            ChangeDir();
            timeBetweenSec = timeBetweenMoves;
        }
    }

    public void ChangeDir()
    {
        int randomPath = Random.Range(0, 5);
        switch (randomPath)
        {
            case 0:
                KeepMoving(Vector3.up, true);
                break;
            case 1:
                KeepMoving(Vector3.down, true);
                break;
            case 2:
                KeepMoving(Vector3.right, true);
                break;
            case 3:
                KeepMoving(Vector3.left, true);
                break;
            case 4:
                KeepMoving(Vector3.zero, false);
                break;
            default:
                break;
        }
    }

    public void Move()
    {
        if (currentState != NpcStates.interact)
        {
            Vector3 temp = transform.position + dirVec * speed * Time.deltaTime;
            if (bound.bounds.Contains(temp))
            {
                myRb.MovePosition(temp);
                AnimationManager();
            }
            else
            {
                ChangeDir();
            }
        }
    }

    private void KeepMoving(Vector3 dir,bool toMove)
    {
        dirVec = dir;
        if (toMove)
        {
            currentState = NpcStates.move;
            myAnim.SetBool("isMoving", true);
        }
        else
        {
            currentState = NpcStates.idle;
            myAnim.SetBool("isMoving", false);
        }
        
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 temp = dirVec;
        ChangeDir();
        while (temp == dirVec)
        {
            ChangeDir();
        }
    }
    
    public void AnimationManager()
    {
        myAnim.SetFloat("moveX", dirVec.x);
        myAnim.SetFloat("moveY", dirVec.y);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                clueSignal.Rise();
                currentState = NpcStates.interact;
                myAnim.SetBool("isMoving", false);
                playerInRange = true;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (playerInRange)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                clueSignal.Rise();
                playerInRange = false;
                dialogBox.SetActive(false);
                currentState = NpcStates.move;
                myAnim.SetBool("isMoving", true);
            }
        }
    }

    public new void Update()
    {
        timeBetweenSec -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = whatDoseTheSignSay;
            }
        }
    }
}

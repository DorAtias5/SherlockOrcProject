using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
public class PlayerMovement : MonoBehaviour
{
    [Header("Vars")]
    public float playerSpeed;
    public float attackCooldown;
    public VectorValue startPos;
    private Vector3 change;

    [Header("Objects Ref")]
    public Rigidbody2D myRigidBody;
    public Animator myAnimator;

    public PlayerState currentState;


    //todo inventory
    public PlayerInventory playerInventory;
    public SpriteRenderer receiveItemSprite;

    //todo player hit mybe on hp
    public Signal playerHit;

    //todo magic
    public FloatValue currentMp;
    public Signal playerMpSignal;
    //todo ability
    public InventoryItem playerBow;
    private bool haveBow;
    public GameObject arrowProj;

    public PlayerHealth playerHealth;

    void Start()
    {
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        transform.position = startPos.defaultValue;
    }


    void FixedUpdate()
    {
        if (currentState == PlayerState.interact)
        {
            return; //dont do anything
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        if (currentState == PlayerState.walk || currentState == PlayerState.idle) { 
            PlayAnimationsAndMove(); 
         }

    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            myAnimator.SetFloat("moveX", change.x);
            myAnimator.SetFloat("moveY", change.y);
            myAnimator.SetBool("moving", true);

        }
        else
        {
            myAnimator.SetBool("moving", false);
        }
    }

    private void Update()
    {
        //Player interactions
        if(currentState == PlayerState.interact)
        {
            return; //dont do anything
        }
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("ability") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if (!haveBow)
            {
                haveBow = CheckItem(playerBow);
                Debug.Log(haveBow);
            }
            if (haveBow)
            {
                StartCoroutine(SecondAttackCo());
            }
        }
        if(playerHealth.currHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change * playerSpeed * Time.deltaTime);
    }

    void PlayAnimationsAndMove()
    {
        if (change != Vector3.zero)
        {
            currentState = PlayerState.walk;
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            myAnimator.SetBool("isMoving", true);
            myAnimator.SetFloat("moveX", change.x);
            myAnimator.SetFloat("moveY", change.y);

        }
        else
        {
            myAnimator.SetBool("isMoving", false);
        }
    }

    #region Coroutines
    private IEnumerator AttackCo()
    {
        myAnimator.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        myAnimator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackCooldown);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    //todo move knockto its own script
    private IEnumerator knockCo(float knockTime)
    {
        //todo hp
        playerHit.Rise();
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        if (currentMp.runTimeValue > 0)
        {
            currentState = PlayerState.attack;
            yield return null;
            //todo ploymorph this from arrow to ability in general
            Vector2 temp = new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY"));
            Arrow arrow = Instantiate(arrowProj, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Fire(ChooseArrowDirection(), temp);
            currentMp.runTimeValue--; //update costs later!!
            //todo mp
            playerMpSignal.Rise();
            yield return new WaitForSeconds(attackCooldown);
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        }
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(knockCo(knockTime));
    }
    #endregion

    public void GetItems()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                myAnimator.SetBool("isHoldingItem", true);
                currentState = PlayerState.interact;
                receiveItemSprite.sprite = playerInventory.currentItem.itemImg;
            }
            else
            {
                myAnimator.SetBool("isHoldingItem", false);
                currentState = PlayerState.idle;
                receiveItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    private Vector3 ChooseArrowDirection()
    {
        float rotation = Mathf.Atan2(myAnimator.GetFloat("moveY"), myAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, rotation);
    }

    public bool CheckItem(InventoryItem item)
    {
        if (playerInventory.playerInv.Contains(item))
        {
            return true;
        }
        return false;
    }
}

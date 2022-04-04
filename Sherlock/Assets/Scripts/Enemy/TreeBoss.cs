using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : Enemy
{
    #region Vars

    [Header("Stages Control")]
    public int currentStage = 0;
        [Header("Speed")]
        public float speedStage1;
        public float speedStage2;
        public float speedStage3;

        [Header("TimeBetweenMoves")]
        public float timeStage1;
        public float timeStage2;
        public float timeStage3;

        [Header("Attacks Cooldown")]
        [SerializeField] private float currentBombCooldown;
        [SerializeField] private float currentWaterCooldown;
        public float bombCooldown2;
        public float bombCooldown3;
        public float waterCooldown1;
        public float waterCooldown3;


    [SerializeField] private bool canAtt;
    [SerializeField] private float roomYedge;
    private bool waterCoActive;
    private bool bombCoActive;

    [Header("Components")]
    public Animator myAnim;
    public Rigidbody2D myRb;
    public GameObject waterProj;
    public GameObject bombProj;

    [Header("Transforms")]
    public Transform waterTotemPos;
    public Transform mainBodyPos;
    public Transform movementPos;

    [Header("Movement")]
    public int currentGoal;
    public Transform[] goals;
    [SerializeField] private float timeBetweenMoves;
    [SerializeField]private float distanceDeviant;
    [SerializeField] private bool coRoutineActive;

    #endregion

    #region Unity Updates and start
    void Start()
    {
        canAtt = true;
        currentState = EnemyState.idle;
    }

    void Update()
    {
        
        if (canAtt)
        {
            switch (currentStage)
            {
                case 1:
                    StartCoroutine(WaterAttCo());
                    break;
                case 2:
                    StartCoroutine(BombAttackCo());
                    break;
                case 3:
                    StartCoroutine(ComboCo());
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
            move();
    }

    #endregion

    #region Moving

    private void move()
    {
        if (currentState != EnemyState.walk)
        {
            currentState = EnemyState.walk;
            int rnd = Random.Range(0, goals.Length);
            currentGoal = rnd;
        }
        if (Vector3.Distance(transform.position, goals[currentGoal].position) > distanceDeviant)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, goals[currentGoal].position, speed * Time.deltaTime);
                myRb.MovePosition(temp);
            }
        else
        {
            if (!coRoutineActive)
            {
                //currentState = EnemyState.idle; no delay
                StartCoroutine(moveDelayCo());
            }
        }
    }

    IEnumerator moveDelayCo()
    {
        coRoutineActive = true;
        yield return new WaitForSeconds(timeBetweenMoves);
        currentState = EnemyState.idle;
        coRoutineActive = false;
    }

    #endregion

    #region Attacks

    public void WaterAttack()
    {
        GameObject proj = Instantiate(waterProj, waterTotemPos.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Fire(Vector3.down);
    }

    IEnumerator WaterAttCo()
    {
        waterCoActive = true;
        canAtt = false;
        WaterAttack();
        yield return new WaitForSeconds(currentWaterCooldown);
        waterCoActive = false;
        canAtt = true;
    }

    public void BombAttack()
    {
        GameObject proj = Instantiate(bombProj, mainBodyPos.position, Quaternion.identity)as GameObject;
        float rnd = Random.Range(2, roomYedge);
        proj.GetComponent<GenericBomb>().timeToStop = rnd;
        proj.GetComponent<Projectile>().Fire(Vector3.down);
    }

    IEnumerator BombAttackCo()
    {
        canAtt = false;
        bombCoActive = true;
        BombAttack();
        yield return new WaitForSeconds(currentBombCooldown);
        canAtt = true;
        bombCoActive = false;
    }

    IEnumerator ComboCo()
    {
        canAtt = false;
        if (!bombCoActive)
        {
            StartCoroutine(BombAttackCo());
            canAtt = true;
        }
        if (!waterCoActive)
        {
            StartCoroutine(WaterAttCo());
            canAtt = true;
        }
        yield return null;
    }

    #endregion

    public void StageTransform(int stage)
    {
        switch (stage)
        {
            case 0:
                //sleep
                break;
            case 1:
                //start water totem
                speed = speedStage1;
                timeBetweenMoves = timeStage1;
                currentStage = stage;
                currentWaterCooldown = waterCooldown1;
                //change animation
                //do attacks every interval
                break;
            case 2:
                //start treetotem
                speed = speedStage2;
                timeBetweenMoves = timeStage2;
                currentStage = stage;
                currentBombCooldown = bombCooldown2;
                //change animation
                //do attacks every interval
                break;
            case 3:
                speed = speedStage3;
                timeBetweenMoves = timeStage3;
                currentStage = stage;
                currentWaterCooldown = waterCooldown3;
                currentBombCooldown = bombCooldown3;
                //start multi totem
                //change animation
                //do both attacks every interval
                break;
            default:
                break;
        }
    }

}

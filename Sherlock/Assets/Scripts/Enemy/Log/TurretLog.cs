using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLog : Log
{
    public GameObject projectile;
    public float fireCooldown;
    private float fireCooldownSec;
    public bool fireReady;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("isAwake", true);
        fireReady = true;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        currentState = EnemyState.idle;
        this.transform.position = homePosition;
    }

    private void Update()
    {
        if (!fireReady)
        {
            fireCooldownSec -= Time.deltaTime;
            if (fireCooldownSec <= 0)
            {
                fireReady = true;
                fireCooldownSec = fireCooldown;
            }
        }
    }
    public override void CheckDist()
    {
        if (Vector3.Distance(target.position, transform.position) <= cheseRadius
             && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                if (fireReady)
                {
                    Vector3 temp = target.transform.position - transform.position; //distance between player and the turret
                    GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                    changeAnimation(temp);
                    proj.GetComponent<Projectile>().Fire(temp.normalized);
                    fireReady = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("isAwake", true);
                }

            }
        }
        else if (Vector3.Distance(target.position, transform.position) > cheseRadius)
        {
            anim.SetBool("isAwake", false);
        }
    }
}

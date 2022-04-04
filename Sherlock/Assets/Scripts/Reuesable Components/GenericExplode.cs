using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericExplode : GenericDestroyOverTime
{
    public Collider2D normalColl;
    public Collider2D explodelColl;
    public GameObject explodeEff;
    [SerializeField] private Animator myAnim;
    private void Start()
    {
        myAnim.SetFloat("Timer", lifeTime);
    }
    public override void Update()
    {
        lifeTime -= Time.deltaTime;
        myAnim.SetFloat("Timer", lifeTime);
        if (lifeTime <= 0)
        {
            //play effect
            GameObject effect = Instantiate(explodeEff, transform.position, Quaternion.identity);
            //expand range
            normalColl.enabled = false;
            explodelColl.enabled = true;
            //dmg player if nearby
            Destroy(effect, 0.5f);
            Destroy(this.gameObject,0.1f);
        }
    }
}

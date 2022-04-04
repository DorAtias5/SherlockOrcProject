using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBomb : Projectile
{
    public float timeToStop;
    public GameObject rangeShadow;
    private bool shadowActive = false;

    // Update is called once per frame
    void Update()
    {
        timeToStop -= Time.deltaTime;
        if (timeToStop <= 0)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //stop
            if (!shadowActive)
            {
                GameObject shadow = Instantiate(rangeShadow, transform.position, Quaternion.identity);
                shadow.transform.parent = this.transform;
                shadowActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.isTrigger)
        {
            //Explode
            this.GetComponent<GenericExplode>().normalColl.enabled = false;
            this.GetComponent<GenericExplode>().explodelColl.enabled = true;
            //dmg player if nearby
            Destroy(this.gameObject,.02f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.GetComponentInChildren<CircleCollider2D>().radius);
    }
}

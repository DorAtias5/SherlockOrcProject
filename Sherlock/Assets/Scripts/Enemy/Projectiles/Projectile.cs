using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Rigidbody2D myRb;
    
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    

    public void Fire(Vector3 direction)
    {
        myRb.velocity = direction * speed;
    }

    /* void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            Destroy(this.gameObject);
        }
    }*/
}

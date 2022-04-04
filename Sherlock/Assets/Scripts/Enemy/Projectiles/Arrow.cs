using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    //arrow
    public void Fire(Vector3 direction , Vector2 velocity)
    {
        myRb.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
}

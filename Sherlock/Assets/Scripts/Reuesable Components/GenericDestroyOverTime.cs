using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDestroyOverTime : MonoBehaviour
{
    public float lifeTime;

    // Update is called once per frame
    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

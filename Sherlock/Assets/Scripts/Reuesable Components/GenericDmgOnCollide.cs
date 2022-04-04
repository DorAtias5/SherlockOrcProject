using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDmgOnCollide : MonoBehaviour
{
    [SerializeField] private string otherTag;
    [SerializeField] private float dmg;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            GenericHealth temp = other.gameObject.GetComponent<GenericHealth>();
            if (temp)
            {
                temp.TakeDmg(dmg);
            }
            Destroy(this.gameObject);
        }
    }
}

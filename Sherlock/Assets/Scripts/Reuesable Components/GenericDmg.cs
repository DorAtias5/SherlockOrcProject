using UnityEngine;

[RequireComponent(typeof(Collider2D))]  //to detect hit
public class GenericDmg : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private string otherTag;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            GenericHealth temp = other.GetComponent<GenericHealth>();
            if (temp)
            {
                temp.TakeDmg(dmg);
            }
        }
    }
}

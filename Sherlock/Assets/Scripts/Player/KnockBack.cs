using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockTime;
    [SerializeField] private float thrust;
    [SerializeField] private string otherTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {

            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.DOMove(hit.transform.position + difference, knockTime);

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger) //for the player
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }
                else if (other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                }
                else if(other.gameObject.CompareTag("Boss") && other.isTrigger)
                {
                    hit.GetComponentInParent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponentInParent<Enemy>().Knock(hit, knockTime);
                }
            }
        }
        /*if (other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("PlayerAttackHitBox"))
        {
            other.GetComponent<Pot>().brakePot();
        }*/
    }

}

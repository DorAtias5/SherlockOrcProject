using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;

    [Header("Vars")]
    public float speed;
    public string enemyName;
    public Vector2 homePosition;

    [Header("Effects")]
    public GameObject deathEffect;
    public float effectTimeToLive;
    public LootTable lootTable;

    public Signal roomSignal;

    public virtual void OnEnable()
    {
        this.transform.position = homePosition;
    }
    
    public void Knock(Rigidbody2D myRb, float knockTime)
    {
        StartCoroutine(knockCo(myRb, knockTime));
    }

    private IEnumerator knockCo(Rigidbody2D myRb, float knockTime)
    {
        if (myRb != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRb.velocity = Vector2.zero;
        }
    }

    public void playDeathEffect()
    {
        if (roomSignal != null)
        {
            roomSignal.Rise();
        }
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectTimeToLive);
        }
    }

    public void MakeItRain()
    {
        if (lootTable != null) 
        {
            PowerUp gonnaSpawn = lootTable.RollForLoot();
            if (gonnaSpawn != null)
            {
                Instantiate(gonnaSpawn.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}

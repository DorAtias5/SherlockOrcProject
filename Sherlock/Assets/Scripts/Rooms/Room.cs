using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("Room Titles")]
    public bool needText;
    public GameObject textBox;
    public Text text;
    public float placeTextLifeTime;
    public string placeName;

    [Header("Stuff to load")]
    public Enemy[] enemiesArr;
    public Pot[] potsArr;
    public GameObject[] npcArr;
    public GameObject virtualCam;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public  virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //enable arrays
            for(int i = 0; i< enemiesArr.Length; i++) //enemies
            {
                ChangeActive(enemiesArr[i], true);
            }
            for (int i = 0; i < potsArr.Length; i++) //pots
            {
                ChangeActive(potsArr[i], true);
            }
            for (int i = 0; i < npcArr.Length; i++) //npc
            {
                ChangeActiveNpc(npcArr[i], true);
            }
            virtualCam.SetActive(true);
            if (needText)
            {
                text = textBox.GetComponent<Text>();
                StartCoroutine(placeTextCo());
            }
        }
        
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //dissable arrays
            for (int i = 0; i < enemiesArr.Length; i++)
            {
                ChangeActive(enemiesArr[i], false);
            }
            for (int i = 0; i < potsArr.Length; i++)
            {
                ChangeActive(potsArr[i], false);
            }
            for (int i = 0; i < npcArr.Length; i++) //npc
            {
                ChangeActiveNpc(npcArr[i], false);
            }
            virtualCam.SetActive(false);
        }
    }

    public void ChangeActive(Component component,bool activation)
    {
        component.gameObject.SetActive(activation);
    }

    public void ChangeActiveNpc(GameObject component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }


    private IEnumerator placeTextCo()
    {
        textBox.SetActive(true);
        text.text = placeName;
        text.CrossFadeAlpha(255, placeTextLifeTime, true);
        text.CrossFadeAlpha(0, placeTextLifeTime, true);
        yield return new WaitForSeconds(placeTextLifeTime);
        textBox.SetActive(false);
    }
}

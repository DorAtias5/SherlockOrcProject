using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour

{
    public Animator potAnim;
    void Start()
    {
        potAnim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void brakePot()
    {
        potAnim.SetBool("broken", true);
        StartCoroutine(brakePotCo());
    }

    private IEnumerator brakePotCo()
    {
        yield return new WaitForSeconds(.33f);
        this.gameObject.SetActive(false);
    }
}

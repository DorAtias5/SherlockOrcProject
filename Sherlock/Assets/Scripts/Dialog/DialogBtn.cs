using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTxt;
    private int choiceVal;
    public void Setup(string NewDialog, int choice)
    {
        myTxt.text = NewDialog;
        choiceVal = choice;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

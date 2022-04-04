using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    [SerializeField] private GameObject dialogCanvas;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject responsePrefab;
    [SerializeField] private GameObject responseContainer;
    [SerializeField] private ScrollRect dialogScroll;
    public bool canContinue;

    [SerializeField] private TextAssetValue dialogVal;
    [SerializeField] private Story story;

    void Start()
    {
        canContinue = true;
    }

    void Update()
    {
    }

    public void DeleteOldDialogs()
    {
        for(int i = 0; i< dialogContainer.transform.childCount; i++)
        {
            Destroy(dialogContainer.transform.GetChild(i).gameObject);
        }
    }

    public void StartCanvas()
    {
        dialogCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void EndCanvas()
    {
        DeleteOldDialogs();
        dialogCanvas.SetActive(false);
    }

    public void SetStory()
    {
        if (dialogVal.Value)
        {
            DeleteOldDialogs();
            story = new Story(dialogVal.Value.text);
        }
        else
        {
            Debug.Log("Story did not make it..");
        }
    }

    void RefreshView()
    {
        while (story.canContinue)
        {
                MakeNewDialog(story.Continue());
        }
        if(story.currentChoices.Count > 0)
        {
            MakeNewChoices();
        }
        else
        {
            dialogCanvas.SetActive(false);
        }
        StartCoroutine(scrollCo());
    }
    
    IEnumerator scrollCo()
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }
    void MakeNewDialog(string newDialog)
    {
        DialogBox tempDialog = 
            Instantiate(dialogPrefab, dialogContainer.transform).GetComponent<DialogBox>();
        tempDialog.Setup(newDialog);
    }

    void MakeNewChoices()
    {
        //destroy old btns
        for(int i=0; i < responseContainer.transform.childCount; i++)
        {
            Destroy(responseContainer.transform.GetChild(i).gameObject);
        }
        //make new ones
        for(int i = 0;  i<story.currentChoices.Count; i++)
        {
            MakeNewResponse(story.currentChoices[i].text, i);
        }
    }

    void MakeNewResponse(string response, int choiseVal)
    {
        DialogBtn tempDialog =
            Instantiate(responsePrefab, responseContainer.transform).GetComponent<DialogBtn>();
        tempDialog.Setup(response, choiseVal);
        Button responseBtn = tempDialog.gameObject.GetComponent<Button>();
        if (responseBtn)
        {
            responseBtn.onClick.AddListener(delegate { ChooseRespond(choiseVal); });
        }
    }

    void ChooseRespond(int choice)
    {
        story.ChooseChoiceIndex(choice);
        RefreshView();
    }
}

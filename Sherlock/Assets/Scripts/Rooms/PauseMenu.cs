using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPause;
    public InventorySaver mySaver;
    public GameObject pausePanel;
    public GameObject invPanel;
    public string mainMenu;
    public bool isInv;
    void Start()
    {
        isPause = false;
        pausePanel.SetActive(false);
        invPanel.SetActive(false);
        isInv = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        isPause = !isPause;
        if (isPause)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            invPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SwitchToPanels()
    {
        if (isPause)
        {
            pausePanel.SetActive(false);
            invPanel.SetActive(true);
            isInv = true;
            isPause = false;
        }
        else
        {
            invPanel.SetActive(false);
            pausePanel.SetActive(true);
            isInv = false;
            isPause = true;
        }
    }

    public void Save()
    {
        mySaver.Save();
    }

    public void ResetSave()
    {
        mySaver.Reset();
    }
}

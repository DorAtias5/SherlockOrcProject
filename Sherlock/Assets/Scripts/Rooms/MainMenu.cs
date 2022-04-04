using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InventorySaver mySaver;
    public string sceneToLoad;
    public void NewGame()
    {
        mySaver.Reset();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadGame()
    {
        mySaver.Load();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

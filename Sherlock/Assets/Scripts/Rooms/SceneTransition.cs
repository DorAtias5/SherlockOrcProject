using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene variables")]
    public string sceneNameToLoad;

    [Header("VectorMem values")]
    public VectorValue oldPlayerPos;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    [Header("RunTime values")]
    public Vector2 newPlayerPos;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;

    [Header("Effects")]
    public GameObject fadeOutPanel;
    public GameObject fadePanel;
    public float fadeTime;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (fadePanel != null){
            GameObject panel = Instantiate(fadePanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger) 
        {
            oldPlayerPos.defaultValue = newPlayerPos;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeTime);
        SetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNameToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void SetCameraBounds()
    {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
}

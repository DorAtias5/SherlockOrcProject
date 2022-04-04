using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    public Vector2 camShift;
    public Vector3 playerShift;
    private CamMovement cam;
    public bool needText;
    public string placeName;
    public GameObject textBox;
    public Text text;
    public float placeTextLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CamMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPos += camShift;
            cam.maxPos += camShift;
            other.transform.position += playerShift;
            if (needText)
            {
                text = textBox.GetComponent<Text>();
                StartCoroutine(placeTextCo());
            }
        }
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

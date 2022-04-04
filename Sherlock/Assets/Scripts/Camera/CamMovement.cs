using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMovement : MonoBehaviour
{
    [Header("Who to follow")]
    public Transform target;

    [Header("Cam controls")]
    public float smoothOffset;
    public Vector2 maxPos; //set it up dynamicly based on the room size
    public Vector2 minPos; // down the devroad

    public Animator camAnim;

    [Header("Position resets")]
    public VectorValue camMin;
    public VectorValue camMax;

    void Start()
    {
        maxPos = camMax.initialValue;
        minPos = camMin.initialValue;
        camAnim = GetComponent<Animator>();
        Vector3 temp = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = temp;
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 tempTarget = new Vector3(target.position.x, target.position.y, transform.position.z);
            tempTarget.x = Mathf.Clamp(tempTarget.x, minPos.x, maxPos.x);
            tempTarget.y = Mathf.Clamp(tempTarget.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, tempTarget, smoothOffset);
        }
    }

    public void ScreenKick()
    {
        camAnim.SetBool("screenKick", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        camAnim.SetBool("screenKick", false);
    }
}

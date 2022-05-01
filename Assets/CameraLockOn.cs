using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public GameObject EndScreenTarget;

    public bool IsEndScreen = false;
    public float EndScreenLerpSpeed = 0.01f;

    private void Update()
    {
        if (IsEndScreen)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, EndScreenTarget.transform.position, EndScreenLerpSpeed);
        }
        else
        {
            if (ObjectToFollow == null)
                return;
            this.transform.position = new Vector3(0, Mathf.Clamp(ObjectToFollow.transform.position.y, 0, Mathf.Infinity), this.transform.position.z);
        }
    }
}

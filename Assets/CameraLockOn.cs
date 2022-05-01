using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    public GameObject ObjectToFollow;

    private void Update()
    {
        if (ObjectToFollow == null)
            return;

        this.transform.position = new Vector3(0, Mathf.Clamp(ObjectToFollow.transform.position.y, 0, Mathf.Infinity), this.transform.position.z);
    }


}

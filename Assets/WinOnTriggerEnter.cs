using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInput playerInput = collision.gameObject.GetComponentInParent<PlayerInput>();

        if (playerInput != null)
        {
            playerInput.SetPlayerInputActive(false);
            DialogManager.Instance.TriggerEndScreen(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<PlayerInput>();
            }

            return _Instance;
        }
    }
    private static PlayerInput _Instance;

    CharacterMover characterMover;

    private void Start()
    {
        characterMover = GetComponent<CharacterMover>();
    }

    private void Update()
    {
        if (characterMover == null)
            return;

        characterMover.SetDesiredDirection(new Vector2(
                Input.GetAxisRaw("Horizontal"),
                0
            ));

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            characterMover.Jump();
        }
    }
}

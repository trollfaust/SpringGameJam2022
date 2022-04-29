using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float MaxSpeed = 5f;
    public float MaxJumpHeight = 10f;

    private Vector2 desiredDirection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        desiredDirection = Vector2.ClampMagnitude(desiredDirection, 1f);

        rb.velocity = new Vector2(desiredDirection.x * MaxSpeed, rb.velocity.y);

    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 1f * MaxJumpHeight);
    }

    public void SetDesiredDirection(Vector2 direction)
    {
        desiredDirection = direction;
    }
}

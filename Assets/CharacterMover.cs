using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float MaxSpeed = 5f;
    public float MaxJumpHeight = 10f;
    public float ShadowOffset = 0.4f;

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

        ShadowCalc();
    }

    private void ShadowCalc()
    {
        RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, Vector2.down);

        Transform shadow = transform.Find("Visuals/Shadow");

        RaycastHit2D hit = results[0];
        float dist = Mathf.Infinity;
        foreach (RaycastHit2D rh in results)
        {
            if (Vector2.SqrMagnitude((Vector2)this.transform.position - rh.point) < dist)
            {
                if (rh.collider == transform.GetComponentInChildren<Collider2D>() || rh.collider.isTrigger)
                    continue;

                dist = Vector2.SqrMagnitude((Vector2)this.transform.position - rh.point);
                hit = rh;
            }
        }

        shadow.transform.position = new Vector3(shadow.transform.position.x, hit.point.y + ShadowOffset, shadow.transform.position.z);
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

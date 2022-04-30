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
    private Animator animator;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        desiredDirection = Vector2.ClampMagnitude(desiredDirection, 1f);
        rb.velocity = new Vector2(desiredDirection.x * MaxSpeed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetTrigger("fall");
        }

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
        if (!isGrounded)
            return;

        rb.velocity = new Vector2(rb.velocity.x, 1f * MaxJumpHeight);
        animator.SetTrigger("jump");
    }

    public void SetDesiredDirection(Vector2 direction)
    {
        desiredDirection = direction;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && (this.transform.position - ((Vector3)collision.collider.offset + collision.transform.position)).y > 0)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }
}

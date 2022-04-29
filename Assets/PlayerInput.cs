using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{


    CharacterMover characterMover;
    Animator animator;

    bool isGrounded = false;

    private void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (characterMover == null)
            return;

        characterMover.SetDesiredDirection(new Vector2(
                Input.GetAxisRaw("Horizontal"),
                0
            ));

        if (Input.GetAxisRaw("Vertical") > 0 && isGrounded)
        {
            characterMover.Jump();
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(1, 1, 1);
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && (this.transform.position - collision.transform.position).y > 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}

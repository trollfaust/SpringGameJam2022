using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject PlatformPrefab;
    private Transform platformParent;

    public Sprite FallenSprite;

    public Collider2D TopCollider;

    public float MinBoxSize = 0.3f;
    public float MinTimeToFall = 0.2f;
    public float FallDelay = 1f;
    public float DelayToNextFall = 0.5f;

    private bool isOqupied = false;
    private bool hasFallen = false;

    private Animator animator;

    private void Start()
    {
        platformParent = FindObjectOfType<PlatformParent>().transform;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        isOqupied = true;

        if (!hasFallen)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        isOqupied = false;
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(MinTimeToFall);

        if (isOqupied)
        {
            StartCoroutine(FallCoroutine());
        }
    }

    IEnumerator FallCoroutine()
    {
        hasFallen = true;
        animator.SetTrigger("Wiggle");
        yield return new WaitForSeconds(FallDelay);

        if (transform.localScale.x > MinBoxSize)
        {
            GameObject newPlatform = Instantiate(PlatformPrefab, platformParent);

            newPlatform.transform.localScale = this.transform.localScale * 0.8f;
            newPlatform.transform.position = this.transform.position;
        }

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = FallenSprite;

        animator.SetTrigger("Fall");

        yield return new WaitForSeconds(DelayToNextFall);
        hasFallen = false;
    }
}

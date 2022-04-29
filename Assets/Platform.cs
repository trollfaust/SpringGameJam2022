using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject PlatformPrefab;
    private Transform platformParent;

    public Sprite FallenSprite;

    public float MinBoxSize = 0.3f;
    public float MinTimeToFall = 0.2f;
    public float MaxTimeToFall = 1f;

    private bool isOqupied = false;
    private bool hasFallen = false;

    private void Start()
    {
        platformParent = FindObjectOfType<PlatformParent>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        isOqupied = true;

        if (!hasFallen)
        {
            StartCoroutine(CountdownCorutine());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        isOqupied = false;
    }

    IEnumerator CountdownCorutine()
    {
        yield return new WaitForSeconds(Random.Range(MinTimeToFall, MaxTimeToFall));

        if (isOqupied)
        {
            Fall();
        }
    }

    void Fall()
    {
        if (transform.localScale.x > MinBoxSize)
        {
            GameObject newPlatform = Instantiate(PlatformPrefab, platformParent);

            newPlatform.transform.localScale = this.transform.localScale * 0.8f;
            newPlatform.transform.position = this.transform.position;
        }

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = FallenSprite;

        hasFallen = true;
    }
}

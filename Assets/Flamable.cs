using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public float LightingTime = 5f;
    public float BurnOutTime = 10f;
    public GameObject SmokePrefab;
    public GameObject FirePrefab;

    //[HideInInspector]
    public bool IsHot = false;

    private bool isGettingHot = false;
    private float countdown = 0f;
    private GameObject smokeEffect;
    private GameObject fireEffect;

    List<Flamable> currentCollisionsFlamable = new List<Flamable>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flamable colliderFlamable = collision.gameObject.GetComponent<Flamable>();

        if (colliderFlamable == null)
            return;

        currentCollisionsFlamable.Add(colliderFlamable);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Flamable colliderFlamable = collision.gameObject.GetComponent<Flamable>();

        if (colliderFlamable == null)
            return;

        currentCollisionsFlamable.Remove(colliderFlamable);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExit2D(collision.collider);
    }

    private void Update()
    {
        bool hasHotCollision = false;

        foreach (Flamable flamable in currentCollisionsFlamable)
        {
            if (flamable.IsHot)
            {
                hasHotCollision = true;
            }
        }

        isGettingHot = hasHotCollision;

        if (isGettingHot && !IsHot)
        {
            StartLighting();

            countdown += Time.deltaTime;
            if (countdown >= LightingTime)
            {
                StartBurning();
            }
        } else
        {
            StopLighting();
            countdown = 0;
        }
    }

    private void StartLighting()
    {
        if (SmokePrefab == null)
            return;

        if (smokeEffect == null)
        {
            smokeEffect = Instantiate(SmokePrefab, this.transform);
        }
        smokeEffect.SetActive(true);
        smokeEffect.transform.position = this.transform.position;
    }

    private void StopLighting()
    {
        if (smokeEffect == null)
            return;
        
        smokeEffect.SetActive(false);
    }

    private void StartBurning() 
    {
        if (FirePrefab == null)
            return;

        IsHot = true;
        if (fireEffect == null)
        {
            fireEffect = Instantiate(FirePrefab, this.transform);
            fireEffect.transform.position = this.transform.position;
            StartCoroutine(BurningCoroutine());
        }
    }

    IEnumerator BurningCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StopLighting();

        yield return new WaitForSeconds(BurnOutTime - 1f);
        Destroy(transform.parent.gameObject);
    }
}

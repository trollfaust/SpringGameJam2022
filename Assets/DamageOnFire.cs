using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnFire : MonoBehaviour
{
    public float FireTickTime = 0.5f;
    public float DamagePerTick = 0.5f;

    private float cooldown = 0;
    private Health health;

    private bool isInHotZone = false;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health == null || !isInHotZone)
            return;

        cooldown += Time.deltaTime;
        if (cooldown >= FireTickTime)
        {
            health.ChangeHealth(-DamagePerTick);
            cooldown = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Flamable flamable = collision.gameObject.GetComponent<Flamable>();

        if (flamable != null)
        {
            if (flamable.IsHot)
            {
                isInHotZone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Flamable flamable = collision.gameObject.GetComponent<Flamable>();

        if (flamable != null)
        {
            isInHotZone = false;
        }
    }
}

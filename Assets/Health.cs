using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 20;
    private float currentHealth;

    private SpriteFlasher spriteFlasher;

    private void Start()
    {
        spriteFlasher = GetComponentInChildren<SpriteFlasher>();
        currentHealth = MaxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp((currentHealth + amount), 0, MaxHealth);

        spriteFlasher?.Flash();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("TODO: Die");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 20;
    private float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;

        Debug.Log("TODO: Damage Indicator");

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

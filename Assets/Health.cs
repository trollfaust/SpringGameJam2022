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
        if (IsDead())
            return;

        currentHealth = Mathf.Clamp((currentHealth + amount), 0, MaxHealth);

        spriteFlasher?.Flash();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        PlayerInput.Instance.SetPlayerInputActive(false);
        GetComponent<Animator>().SetBool("isDead", true);

        yield return new WaitForSeconds(2f);

        DialogManager.Instance.TriggerEndScreen(false);
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}

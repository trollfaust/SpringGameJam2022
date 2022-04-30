using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Sprite BrokenHeartSprite;
    public Image RedHeart;
    public Image BlackHeart;

    Health playerHealth;

    private void Start()
    {
        playerHealth = PlayerInput.Instance.GetComponent<Health>();
    }

    private void Update()
    {
        if (playerHealth == null)
            return;

        RedHeart.fillAmount = playerHealth.GetHealth() / playerHealth.GetMaxHealth();

        if (playerHealth.GetHealth() <= 0)
        {
            BlackHeart.sprite = BrokenHeartSprite;
        }
    }
}

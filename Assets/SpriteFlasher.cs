using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
	public Color FlashColor;
	public float FlashDuration;

	Material material;

	private IEnumerator flashCoroutine;

	private void Awake()
	{
		material = GetComponent<SpriteRenderer>().material;
	}

	private void Start()
	{
		material.SetColor("_FlashColor", FlashColor);
	}

	public void Flash()
	{
		if (flashCoroutine != null)
			StopCoroutine(flashCoroutine);

		flashCoroutine = DoFlash();
		StartCoroutine(flashCoroutine);
	}

	private IEnumerator DoFlash()
	{
		float lerpTime = 0;

		while (lerpTime < FlashDuration)
		{
			lerpTime += Time.deltaTime;
			float perc = lerpTime / FlashDuration;

			SetFlashAmount(1f - perc);
			yield return null;
		}
		SetFlashAmount(0);
	}

	private void SetFlashAmount(float flashAmount)
	{
		material.SetFloat("_FlashAmount", flashAmount);
	}
}

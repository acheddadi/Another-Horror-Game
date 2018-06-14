using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodOverlay : MonoBehaviour
{
	[SerializeField]private Texture[] bloodTexture;
	[SerializeField]private float minOpacity = 0.5f, maxOpacity = 0.8f;
	private RawImage img;
	private Texture originalTexture;
	private int index = 0, count = 0;
	private float increments;
	private bool direction = true;

	void Start()
	{
		img = GetComponent<RawImage>();
		increments = 100 / bloodTexture.Length;
	}

	void Update()
	{
		if (direction)
		{
			if (img.color.a > 0.5f)
			{
				float alpha = Mathf.Clamp(img.color.a - minOpacity * Time.deltaTime, minOpacity, maxOpacity);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}
		else
		{
			if (img.color.a < 0.8f)
			{
				float alpha = Mathf.Clamp(img.color.a + minOpacity * Time.deltaTime, minOpacity, maxOpacity);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}

		int damage = 100 - GameController.health;
		index = Mathf.Clamp(Mathf.CeilToInt(damage / increments) - 1, 0, bloodTexture.Length - 1);
		img.texture = bloodTexture[index];
	}

	void OnDestroy()
	{
		img.texture = originalTexture;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodOverlay : MonoBehaviour
{
	[SerializeField]private Texture[] bloodTexture;
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
				float alpha = Mathf.Clamp(img.color.a - 0.5f * Time.deltaTime, 0.5f, 1.0f);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}
		else
		{
			if (img.color.a < 1.0f)
			{
				float alpha = Mathf.Clamp(img.color.a + 0.5f * Time.deltaTime, 0.5f, 1.0f);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}
	}

	void OnDestroy()
	{
		img.texture = originalTexture;
	}

	public void DisplayOverlay(int health)
	{
		int damage = 100 - health;
		index = Mathf.Clamp(Mathf.CeilToInt(damage / increments) - 1, 0, bloodTexture.Length - 1);
		img.texture = bloodTexture[index];
	}
}

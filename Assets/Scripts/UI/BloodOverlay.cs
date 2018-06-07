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
				float alpha = Mathf.Clamp(img.color.a - Time.deltaTime, 0.5f, 1.0f);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}
		else
		{
			if (img.color.a < 1.0f)
			{
				float alpha = Mathf.Clamp(img.color.a + Time.deltaTime, 0.5f, 1.0f);
				img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			}
			else direction = !direction;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log(count);
			switch (count)
			{
				case 0:
				NextTexture(100);
				break;
				case 1:
				NextTexture(90);
				break;
				case 2:
				NextTexture(80);
				break;
				case 3:
				NextTexture(70);
				break;
				case 4:
				NextTexture(60);
				break;
				case 5:
				NextTexture(50);
				break;
				case 6:
				NextTexture(40);
				break;
				case 7:
				NextTexture(30);
				break;
				case 8:
				NextTexture(20);
				break;
				case 9:
				NextTexture(10);
				break;
				case 10:
				NextTexture(0);
				break;
			}
			if (count > 9) count = 0;
			else count++;
		}
	}

	void OnDestroy()
	{
		img.texture = originalTexture;
	}

	void NextTexture(int health)
	{
		int damage = 100 - health;
		index = Mathf.Clamp(Mathf.RoundToInt(damage / increments) - 1, 0, bloodTexture.Length - 1);
		img.texture = bloodTexture[index];
	}
}

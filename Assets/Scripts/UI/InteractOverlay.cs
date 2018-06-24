using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractOverlay : MonoBehaviour
{
	private Image img;
	// Use this for initialization
	void Start()
	{
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (GameController.leftClick && img.color.a < 1.0f) img.color = new Color (img.color.r, img.color.g, img.color.b, Mathf.Clamp(img.color.a + Time.unscaledDeltaTime, 0.0f, 1.0f));
		else if (img.color.a > 0.0f) img.color = new Color (img.color.r, img.color.g, img.color.b, Mathf.Clamp(img.color.a - Time.unscaledDeltaTime, 0.0f, 1.0f));
	}
}

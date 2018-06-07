using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWobble : MonoBehaviour
{
	[SerializeField]private float wobbleStrength = 5.0f;
	private const int MULTIPLIER = 100;
	private bool isWobbling = false;
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Wobble());
	}

	private IEnumerator Wobble()
	{
		if (!isWobbling)
		{
			isWobbling = true;
			Vector3 initRot = transform.localEulerAngles;
			for (float i = 0.0f; i < 1.0f; i += 0.1f)
			{
				transform.localEulerAngles = initRot + Vector3.forward * i * wobbleStrength * MULTIPLIER * Time.deltaTime;
				yield return null;
			}
			transform.localEulerAngles = initRot + Vector3.forward * wobbleStrength;
			for (float i = -1.0f; i < 0.0f; i += 0.1f)
			{
				transform.localEulerAngles = initRot + Vector3.forward * i * wobbleStrength * MULTIPLIER * Time.deltaTime;
				yield return null;
			}
			transform.localEulerAngles = initRot;
			isWobbling = false;
		}
	}
}

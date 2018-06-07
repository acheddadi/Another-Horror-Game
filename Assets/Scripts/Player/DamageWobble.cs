using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWobble : MonoBehaviour
{
	[SerializeField]private float wobbleStrength = 5.0f;
	[SerializeField]private AudioClip[] hurtClips;
	private AudioSource source;
	private PlayerController player;
	private const int MULTIPLIER = 100;
	private bool isWobbling = false;

	void Start()
	{
		player = GetComponentInParent<PlayerController>();
		source = GetComponent<AudioSource>();
	}

	public void ReactToHurt()
	{
		StartCoroutine(Wobble());
	}

	private IEnumerator Wobble()
	{
		if (!isWobbling)
		{
			isWobbling = true;
			Vector3 initRot = transform.localEulerAngles;
			for (float i = 0.0f; i < wobbleStrength; i += Time.deltaTime * MULTIPLIER)
			{
				transform.localEulerAngles = initRot + (Vector3.forward * Mathf.Clamp(i, 0.0f, wobbleStrength));
				yield return null;
			}
			if ((hurtClips.Length > 0) && (player != null) && (source != null))
			{
				int rndNmb = Random.Range(0, hurtClips.Length - 1);
				source.clip = hurtClips[rndNmb];
				source.Play();
			}
			transform.localEulerAngles = initRot + Vector3.forward * wobbleStrength;
			for (float i = 0.0f; i < wobbleStrength; i += Time.deltaTime * MULTIPLIER)
			{
				transform.localEulerAngles = initRot + (Vector3.forward * -1 * Mathf.Clamp(i, 0.0f, wobbleStrength));
				yield return null;
			}
			transform.localEulerAngles = initRot;
			isWobbling = false;
		}
	}
}

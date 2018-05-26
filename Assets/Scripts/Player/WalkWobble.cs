using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WalkWobble : MonoBehaviour
{
	[SerializeField]private float wobbleAmount = 1.0f, wobbleSpeed = 1.0f;
	[SerializeField]private bool audibleFootsteps = false;
	[SerializeField]private AudioClip[] footstepClips;
	private PlayerController player;
	private CharacterController character;
	private AudioSource source;
	private float spd, currentWobble = 0.0f;
	private bool isWobbling = false;

	void Start()
	{
		if (audibleFootsteps)
		{
			player = GetComponentInParent<PlayerController>();
			source = GetComponent<AudioSource>();
			if (source == null) Debug.LogError("AudioSource Component is missing!");
		}
		
		spd = wobbleSpeed;
	}

    void Update()
	{
		if (player != null)
		{
			if (player.IsRunning()) spd = wobbleSpeed * player.RunMultiplier();
			else spd = wobbleSpeed;
		}
		
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			if (currentWobble < wobbleAmount) currentWobble += spd * Time.deltaTime;
			if (!isWobbling) StartCoroutine(StartWobble());
		}
		else if (currentWobble > 0) currentWobble -= spd * Time.deltaTime;
		else if (currentWobble < 0) currentWobble = 0;
     }

	 IEnumerator StartWobble()
	 {
		isWobbling = true;
		float wobblePos = 0.0f;
		Vector3 initPos = transform.localPosition;
		while ((wobblePos < currentWobble) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
		{
			wobblePos += spd * Time.deltaTime;
			transform.localPosition = initPos + new Vector3(0.0f, wobblePos, 0.0f);
			yield return null;
		}
		while (wobblePos > 0.0f)
		{
			wobblePos -= spd * Time.deltaTime * 2;
			transform.localPosition = initPos + new Vector3(0.0f, wobblePos, 0.0f);
			yield return null;
		}
		transform.localPosition = initPos;
		isWobbling = false;
		if ((footstepClips.Length > 0) && audibleFootsteps && (player != null) && (source != null))
		{
			int rndNmb = Random.Range(0, footstepClips.Length - 1);
			source.clip = footstepClips[rndNmb];
			source.Play();
		}
	 }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShatter : MonoBehaviour
{
	[SerializeField] private GameObject enemy, intactGlass, brokenGlass;
	[SerializeField] private DoubleSlidingDoorController door;
	private AudioSource sfx;

	void Start()
	{
		sfx = GetComponent<AudioSource>();
	}

	public void ShatterGlass()
	{
		door.UnlockDoor();
		sfx.Play();
		enemy.SetActive(true);
		intactGlass.SetActive(false);
		brokenGlass.SetActive(true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
	private PlayerController player;
	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player != null)
		{
			transform.position = player.transform.position;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAim : MonoBehaviour
{
	private Animator anime;
	private CameraController camRot;

	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator>();
		camRot = GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton(1))
		{
			anime.SetBool("RightClick", true);
			camRot.FPS(true);
		}
		else
		{
			anime.SetBool("RightClick", false);
			camRot.FPS(false);
		}
		
	}
}

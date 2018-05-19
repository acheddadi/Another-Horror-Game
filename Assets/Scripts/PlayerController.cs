using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]private bool enableGravity = true;
	[SerializeField]private float movementSpeed = 3.0f, runMultiplier = 2.0f, gravity = 9.8f;
	private CharacterController player;
	private bool isRunning = false;
	private float spd;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
		else isRunning = false;
		if (isRunning) spd = movementSpeed * runMultiplier;
		else spd = movementSpeed;

		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		movement = transform.TransformDirection(movement);
		movement *= spd * Time.deltaTime;
		if (enableGravity) movement.y -= gravity * Time.deltaTime;
		player.Move(movement);
	}

	public bool IsRunning()
	{
		return isRunning;
	}

	public float RunMultiplier()
	{
		return runMultiplier;
	}
}

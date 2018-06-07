using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]private bool enableGravity = true;
	[SerializeField]private float movementSpeed = 3.0f, runMultiplier = 2.0f, gravity = 9.8f;
	[SerializeField]private float distanceToInteractable = 3.0f, interactionRadius = 3.0f, invincibilityCooldown = 1.0f;
	private CharacterController player;
	private Camera cam;
	private bool isRunning = false;
	private float spd, lastCooldown;
	private int health = 100;
	private BloodOverlay healthDisplay;
	private DamageWobble hurtEffect;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<CharacterController>();
		cam = Camera.main;
		healthDisplay = FindObjectOfType<BloodOverlay>();
		hurtEffect = GetComponentInChildren<DamageWobble>();
		lastCooldown = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
		if (Physics.SphereCast(ray, interactionRadius, out hit, distanceToInteractable))
		{
			InteractableController interactable;
			interactable = hit.transform.gameObject.GetComponent<InteractableController>();
			if (interactable != null)
			{
				interactable.Highlight();
				if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !GameController.gameIsPaused) interactable.Interact();
			}
		}

		if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
		else isRunning = false;
		if (isRunning) spd = movementSpeed * runMultiplier;
		else spd = movementSpeed;

		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		movement = transform.TransformDirection(movement.normalized);
		movement *= spd * Time.deltaTime;
		if (enableGravity) movement.y -= gravity * Time.deltaTime;
		player.Move(movement);
		healthDisplay.DisplayOverlay(health);
	}

	public bool IsRunning()
	{
		return isRunning;
	}

	public float RunMultiplier()
	{
		return runMultiplier;
	}

	public void TakeDamage(int damage)
	{
		if (Time.time > lastCooldown + invincibilityCooldown)
		{
			health -= damage;
			hurtEffect.ReactToHurt();
			lastCooldown = Time.time;
			Debug.Log(health);
		}
		
	}
}

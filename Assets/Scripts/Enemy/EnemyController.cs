using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	[SerializeField]private float wanderDistance = 5.0f, recurringTimer = 5.0f;
	[SerializeField]private float walkSpeed = 1.25f, runMultiplier = 2.0f, acceleration = 7.0f;
	private Animator anime;
	private NavMeshAgent nav;
	private float timer, speed, accel;
	private Vector3 randomPos;
	private bool isRunning = false;
	private float velocity = 0.0f, velocity2 = 0.0f, smoothTime = 0.3f;

	// Use this for initialization
	void Start()
	{
		anime = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		anime.SetFloat("Random Walk", Random.Range(0.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update()
	{
		if (timer < 0 && !isRunning)
		{
			do
			{
				randomPos = Random.insideUnitCircle * wanderDistance;
				randomPos += transform.position;
			} while (!nav.SetDestination(randomPos));
			timer = recurringTimer;
		}

		if (isRunning)
		{
			speed = walkSpeed * runMultiplier;
			accel = acceleration * runMultiplier;
			float animSpd = Mathf.SmoothDamp
			(
				anime.GetFloat("Speed"),
				nav.velocity.magnitude / nav.speed,
				ref velocity2, smoothTime
			);
			anime.SetFloat("Speed", animSpd);
		}
		else
		{
			speed = walkSpeed;
			accel = acceleration;
			float animSpd = Mathf.SmoothDamp
			(
				anime.GetFloat("Speed"),
				Mathf.Clamp(nav.velocity.magnitude / nav.speed, 0.0f, 0.5f),
				ref velocity2, smoothTime
			);
			anime.SetFloat("Speed", animSpd);
		}

		if ((nav.destination - transform.position).magnitude <= nav.stoppingDistance) isRunning = false;

		nav.speed = Mathf.SmoothDamp(nav.speed, speed, ref velocity, smoothTime);
		nav.acceleration = accel;
		timer -= Time.deltaTime;
	}

	void OnTriggerStay(Collider other)
	{
		RaycastHit info;
		Vector3 otherDir = other.transform.position - transform.position;
		Physics.Raycast(transform.position, otherDir, out info, Mathf.Infinity);
		PlayerController player = info.transform.GetComponent<PlayerController>();
		if (player != null)
		{
			nav.destination = player.transform.position;
			if ((nav.destination - transform.position).magnitude > nav.stoppingDistance) isRunning = true;
			timer = recurringTimer;
		}
	}

	public void TakeDamage()
	{
		
	}

	private void Death()
	{

	}
}

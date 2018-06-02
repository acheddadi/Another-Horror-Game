﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	[SerializeField]private int health = 90, staggerDamage = 30, criticalMultiplier = 3;
	[SerializeField]private float wanderDistance = 5.0f, recurringTimer = 5.0f;
	[SerializeField]private float walkSpeed = 1.25f, runMultiplier = 2.0f, acceleration = 7.0f, staggerTime = 1.5f;
	[SerializeField]private Collider criticalHit;
	private Animator anime;
	private NavMeshAgent nav;
	private float timer, speed, accel;
	private Vector3 randomPos;
	private bool isRunning = false, isStaggered = false;
	private float velocity = 0.0f, velocity2 = 0.0f, smoothTime = 0.3f;
	private int builtUpDamage = 0;

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
		if (timer < 0 && !isRunning && !isStaggered)
		{
			do
			{
				randomPos = Random.insideUnitCircle * wanderDistance;
				randomPos.z = randomPos.y; randomPos.y = 0.0f;
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
			timer -= Time.deltaTime;
		}

		if ((nav.destination - transform.position).magnitude <= nav.stoppingDistance) isRunning = false;

		anime.SetBool("isRunning", isRunning);
		nav.speed = Mathf.SmoothDamp(nav.speed, speed, ref velocity, smoothTime);
		nav.acceleration = accel;
	}

	void OnTriggerStay(Collider other)
	{
		RaycastHit info;
		Vector3 otherDir = other.transform.position - transform.position;
		Physics.Raycast(transform.position, otherDir, out info, Mathf.Infinity);
		PlayerController player = info.transform.GetComponent<PlayerController>();
		if (player != null) Attract(player.transform.position);
	}

	public void Attract(Vector3 position)
	{
		nav.destination = position;
		if ((nav.destination - transform.position).magnitude > nav.stoppingDistance) isRunning = true;
		timer = recurringTimer;
	}

	public void TakeDamage(Collider bodyPart, int dmg)
	{
		int damage;
		if (bodyPart == criticalHit) damage = dmg * criticalMultiplier;
		else damage = dmg;
		builtUpDamage += damage;
		health -= damage;

		if (builtUpDamage >= staggerDamage)
		{
			anime.SetFloat("Random Damage", Random.Range(0.0f, 1.0f));
			anime.SetTrigger("Damage");
			StartCoroutine(PauseNavigation());
			builtUpDamage = 0;
		}

		if (health <= 0) Death();
	}

	private void Death()
	{
		Destroy(GetComponent<EnemyController>());
	}

	private void OnDestroy()
	{
		Destroy(GetComponent<Animator>());
		Destroy(GetComponent<NavMeshAgent>());
		Destroy(GetComponentInChildren<LookAt>().gameObject);
		Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in rbs) rb.isKinematic = false;
	}

	private IEnumerator PauseNavigation()
	{
		isStaggered = true;
		nav.isStopped = true;
		nav.velocity = Vector3.zero;
		yield return new WaitForSeconds(staggerTime);
		nav.isStopped = false;
		isStaggered = false;
	}
}
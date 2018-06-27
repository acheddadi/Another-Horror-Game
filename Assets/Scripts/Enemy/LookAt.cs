using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAt : MonoBehaviour
{
	void OnTriggerStay(Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if (player != null)
		{
			NavMeshAgent nav = GetComponentInParent<NavMeshAgent>();
			nav.isStopped = true;
			Vector3 playerDir = player.transform.position - nav.transform.position; playerDir.y = 0.0f;
			nav.transform.rotation = Quaternion.RotateTowards(nav.transform.rotation, Quaternion.LookRotation(playerDir), 1.0f);

			EnemyController enemy = GetComponentInParent<EnemyController>();
			enemy.Attack();
		}
	}

	void OnTriggerExit(Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if (player != null)
		{
			NavMeshAgent nav = GetComponentInParent<NavMeshAgent>();
			nav.isStopped = false;
		}
	}
}

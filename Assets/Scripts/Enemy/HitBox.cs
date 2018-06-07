using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	[SerializeField]private int damageAmount = 10;
	
	void OnTriggerEnter(Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		EnemyController enemy = GetComponentInParent<EnemyController>();
		if (player != null && enemy.IsAttacking())
		{
			player.TakeDamage(damageAmount);
			enemy.HitSFX();
		}
	}
}

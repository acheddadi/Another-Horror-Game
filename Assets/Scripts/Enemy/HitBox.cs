using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	[SerializeField]private int damageAmount = 10;
	
	void OnTriggerEnter(Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if (player != null) player.TakeDamage(damageAmount);
	}
}

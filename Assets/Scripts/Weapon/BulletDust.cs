using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDust : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		StartCoroutine(TimedDestroy());
	}

	private IEnumerator TimedDestroy()
	{
		yield return new WaitForSeconds (2.0f);
		Destroy(gameObject);
	}
}

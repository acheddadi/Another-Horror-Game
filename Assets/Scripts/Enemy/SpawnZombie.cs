using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
	[SerializeField]private GameObject zombieObject;
	private Animator anime;
	// Use this for initialization
	void Start()
	{
		anime = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		AnimatorStateInfo info = anime.GetCurrentAnimatorStateInfo(0);
		if (info.normalizedTime > 0.9f)
		{
			zombieObject.SetActive(true);
			Destroy(gameObject);
		}
	}
}

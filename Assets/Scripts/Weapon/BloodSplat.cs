using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
	Animator anime;
	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator>();
		anime.SetInteger("choice", Random.Range(0, 3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

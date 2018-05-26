using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	[SerializeField]private GameObject bulletHole;
	[SerializeField]private Transform ironSight;
	private Camera cam;
	private LayerMask mask;

	void Start()
	{
		cam = Camera.main;
		mask = LayerMask.GetMask("Default");
	}

	public void Fire()
	{
		RaycastHit info;
		Ray ray = cam.ViewportPointToRay(cam.WorldToViewportPoint(ironSight.position));
		if (Physics.Raycast(ray, out info,float.PositiveInfinity, mask))
		{
        	GameObject bh = Instantiate(bulletHole, info.point, Quaternion.LookRotation(info.normal));
			bh.transform.localPosition += Vector3.back * 0.001f;
			bh.SetActive(true);
			bh.transform.parent = GameObject.FindObjectOfType<SpawnedInstances>().transform;
		}
	}
}

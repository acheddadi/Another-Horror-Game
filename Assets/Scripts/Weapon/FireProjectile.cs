using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	[SerializeField]private GameObject bulletDust, bloodSplat;
	[SerializeField]private Transform ironSight;
	[SerializeField]private float bulletForce = 3.0f;
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
			GameObject toInstantiate;
			if (info.transform.GetComponent<EnemyTag>()) toInstantiate = bloodSplat;
			else toInstantiate = bulletDust;

        	GameObject obj = Instantiate(toInstantiate, info.point, Quaternion.LookRotation(info.normal));
			obj.transform.localPosition += Vector3.back * 0.001f;
			obj.SetActive(true);
			obj.transform.parent = GameObject.FindObjectOfType<SpawnedInstances>().transform;

			if (info.transform.GetComponent<Rigidbody>() != null)
			{
				Rigidbody rb = info.transform.GetComponent<Rigidbody>();
				rb.AddForce(ray.direction * bulletForce);
			}

		}
	}
}

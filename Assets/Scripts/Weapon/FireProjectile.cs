using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	[SerializeField]private GameObject bulletDust, bloodSplat;
	[SerializeField]private Transform ironSight;
	[SerializeField]private float bulletForce = 3.0f;
	[SerializeField]private int damage = 10;
	private Camera cam;
	LayerMask mask;

	void Start()
	{
		cam = Camera.main;
		mask = ~LayerMask.GetMask("Player", "Ignore Raycast");
	}

	public void Fire()
	{
		RaycastHit info;
		Ray ray = cam.ViewportPointToRay(cam.WorldToViewportPoint(ironSight.position));
		if (Physics.Raycast(ray, out info,float.PositiveInfinity, mask))
		{
			GameObject toInstantiate;
			if (info.transform.GetComponentInParent<EnemyController>())
			{
				toInstantiate = bloodSplat;
				EnemyController enemy = info.transform.GetComponentInParent<EnemyController>();
				enemy.TakeDamage(info.transform.GetComponent<Collider>(), damage);
			}
			else if (info.transform.GetComponentInParent<EnemyTag>()) toInstantiate = bloodSplat;
			else toInstantiate = bulletDust;
			
        	GameObject obj = Instantiate(toInstantiate, info.point, Quaternion.LookRotation(info.normal));
			obj.transform.localPosition += Vector3.back * 0.001f;
			obj.SetActive(true);
			obj.transform.parent = GameObject.FindObjectOfType<SpawnedInstances>().transform;

			if (info.transform.GetComponent<Rigidbody>() != null)
			{
				Rigidbody rb = info.transform.GetComponent<Rigidbody>();
				StartCoroutine(KnockBack(rb, ray));
			}
		}
		EnemyController[] enemyHeard = FindObjectsOfType<EnemyController>();
		foreach (EnemyController enemy in enemyHeard)
		{
			Collider collider = GetComponent<Collider>();
			if (collider.bounds.Contains(enemy.transform.position)) enemy.Attract(transform.position);
		}
	}

	private IEnumerator KnockBack(Rigidbody rb, Ray ray)
	{
		yield return new WaitForEndOfFrame();
		rb.AddForce(ray.direction * bulletForce);
	}
}

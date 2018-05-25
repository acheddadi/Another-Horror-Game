using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAim : MonoBehaviour
{
	[SerializeField]private float fireRate = 0.1f, recoilAmount = 1.0f;
	private Animator anime;
	private CameraController camRot;
	private Camera camObj;
	private float initZoom;
    private float velocity = 0.0f, smoothTime = 0.1f;
	private float lastShot = 0.0f;
	private bool recoiling = false;

	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator>();
		camRot = GetComponent<CameraController>();
		camObj = FindObjectOfType<Camera>();
		initZoom = camObj.fieldOfView;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameController.gameIsPaused)
		{
			if (Input.GetMouseButton(1))
			{
				anime.SetBool("RightClick", true);
				camRot.FPS(true);
				camObj.fieldOfView = Mathf.SmoothDamp(camObj.fieldOfView, initZoom - 20, ref velocity, smoothTime);
				if (Input.GetMouseButtonDown(0) && (lastShot > fireRate))
				{
					anime.SetTrigger("LeftClick");
					StartCoroutine(RecoilCamera());
					lastShot = 0.0f;
				}
			}
			else
			{
				anime.SetBool("RightClick", false);
				camRot.FPS(false);
				camObj.fieldOfView = Mathf.SmoothDamp(camObj.fieldOfView, initZoom, ref velocity, smoothTime);
			}
			lastShot += Time.deltaTime;
		}
	}

	private IEnumerator RecoilCamera()
	{
		if (!recoiling)
		{
			recoiling = true;
			Vector3 initRot = camObj.transform.localEulerAngles;
			Vector3 rot = initRot;

			while (rot.x < initRot.x + recoilAmount)
			{
				rot.x += 0.25f;
				camObj.transform.localEulerAngles = rot;
				yield return null;
			}

			while (rot.x > initRot.x)
			{
				rot.x -= 0.25f;
				camObj.transform.localEulerAngles = rot;
				yield return null;
			}
			recoiling = false;
		}
	}
}

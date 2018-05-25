using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAim : MonoBehaviour
{
	[SerializeField]private float fireRate = 0.1f, recoilAmount = 1.0f;
	private Animator anime;
	private CameraController camRot;
	private Camera camObj;
	private AudioSource sfx;
	private float initZoom, velocity = 0.0f, smoothTime = 0.1f, lastShot = 0.0f;
	private bool recoiling = false, letGo = true;
	private int lastClick;

	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator>();
		camRot = GetComponent<CameraController>();
		camObj = FindObjectOfType<Camera>();
		sfx = GetComponent<AudioSource>();
		initZoom = camObj.fieldOfView;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameController.gameIsPaused)
		{
			if (Input.GetMouseButtonDown(0)) lastClick = 0;
			if (Input.GetMouseButtonDown(1)) lastClick = 1;
			if (Input.GetMouseButton(1))
			{
				anime.SetBool("RightClick", true);
				camRot.FPS(true);
				camObj.fieldOfView = Mathf.SmoothDamp(camObj.fieldOfView, initZoom - 20, ref velocity, smoothTime);
				
				if (Input.GetMouseButton(0) && (lastShot > fireRate) && letGo)
				{
					AnimatorStateInfo info = anime.GetCurrentAnimatorStateInfo(0);
					AnimatorStateInfo nextInfo = anime.GetNextAnimatorStateInfo(0);
					if ((info.normalizedTime > 1.0f) && (nextInfo.normalizedTime == 0.0f) && (info.IsName("Base.PointGun")) && (lastClick == 0))
					{
						anime.SetTrigger("LeftClick");
						StartCoroutine(RecoilCamera());
						sfx.pitch = Random.Range(0.9f, 1.1f);
						sfx.Play();
						lastShot = 0.0f;
						letGo = false;
					}
				}
			}
			else
			{
				anime.SetBool("RightClick", false);
				camRot.FPS(false);
				camObj.fieldOfView = Mathf.SmoothDamp(camObj.fieldOfView, initZoom, ref velocity, smoothTime);
			}
			if (Input.GetMouseButtonUp(0)) letGo = true;
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

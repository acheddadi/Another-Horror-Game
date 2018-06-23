using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlidingDoorController : MonoBehaviour
{
	[SerializeField] private AudioClip doorOpeningSoundClip, doorClosingSoundClip, doorAccessDeniedClip;
	[SerializeField] private Transform halfDoorLeftTransform, halfDoorRightTransform;	//	Left panel of the sliding door
	[SerializeField] private float slideDistance = 0.88f, speed = 1f;
	[SerializeField] private enum DoubleSlidingDoorStatus { Closed, Open, Animating, Locked }
	[SerializeField] private Renderer doorPanel;
	[SerializeField] private bool locked;
	
	private DoubleSlidingDoorStatus status;
	private Vector3 leftDoorClosedPosition, leftDoorOpenPosition, rightDoorClosedPosition, rightDoorOpenPosition;
	private int objectsOnDoorArea	= 0;
	private AudioSource audioSource;
	private Material panelMat;
	private Color panelDefaultColor;


	// Use this for initialization
	void Start ()
	{
		leftDoorClosedPosition = rightDoorClosedPosition = new Vector3 (0f, 0f, 0f);
		leftDoorOpenPosition = new Vector3 (0f, 0f, slideDistance);
		rightDoorOpenPosition = new Vector3 (0f, 0f, -slideDistance);
		audioSource = GetComponent<AudioSource>();
		panelMat = doorPanel.materials[1];
		panelDefaultColor = panelMat.GetColor("_EmissionColor");

		if (locked) status = DoubleSlidingDoorStatus.Locked;
		else UnlockDoor();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (status != DoubleSlidingDoorStatus.Animating)
		{
			if (status == DoubleSlidingDoorStatus.Open)
			{
				if (objectsOnDoorArea == 0) StartCoroutine(CloseDoors());
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (status != DoubleSlidingDoorStatus.Animating)
		{
			if (status == DoubleSlidingDoorStatus.Closed) StartCoroutine(OpenDoors());
			else if (status == DoubleSlidingDoorStatus.Locked) StartCoroutine(AccessDenied());
		}

		if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer ("Player")) objectsOnDoorArea++;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer ("Player")) objectsOnDoorArea--;
	}

	private IEnumerator OpenDoors ()
	{
		panelMat.SetColor("_EmissionColor", new Color(0.0f, 1.0f, 0.0f));
		if (doorOpeningSoundClip != null) audioSource.PlayOneShot (doorOpeningSoundClip, 0.7F);

		status = DoubleSlidingDoorStatus.Animating;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * speed)
		{
			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorClosedPosition, leftDoorOpenPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorClosedPosition, rightDoorOpenPosition, t);
			yield return null;
		}

		status = DoubleSlidingDoorStatus.Open;
	}

	private IEnumerator CloseDoors ()
	{
		panelMat.SetColor("_EmissionColor", panelDefaultColor);
		if (doorClosingSoundClip != null) audioSource.PlayOneShot(doorClosingSoundClip, 0.7F);

		status = DoubleSlidingDoorStatus.Animating;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * speed)
		{
			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorOpenPosition, leftDoorClosedPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorOpenPosition, rightDoorClosedPosition, t);
			yield return null;
		}

		status = DoubleSlidingDoorStatus.Closed;
	}

	private IEnumerator AccessDenied()
	{
		panelMat.SetColor("_EmissionColor", new Color(1.0f, 0.0f, 0.0f));
		audioSource.PlayOneShot(doorAccessDeniedClip, 0.4f);
		yield return new WaitForSeconds(2.0f);
		panelMat.SetColor("_EmissionColor", panelDefaultColor);
	}

	//	Forced door opening
	public bool DoOpenDoor ()
	{
		if (status != DoubleSlidingDoorStatus.Animating)
		{
			if (status == DoubleSlidingDoorStatus.Closed)
			{
				StartCoroutine(OpenDoors());
				return true;
			}
		}
		return false;
	}

	//	Forced door closing
	public bool DoCloseDoor ()
	{
		if (status != DoubleSlidingDoorStatus.Animating)
		{
			if (status == DoubleSlidingDoorStatus.Open)
			{
				StartCoroutine(CloseDoors());
				return true;
			}
		}
		return false;
	}

	public void UnlockDoor()
	{
		if (status == DoubleSlidingDoorStatus.Locked) status = DoubleSlidingDoorStatus.Closed;
	}
}

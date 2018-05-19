using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform cameraPosition;
	private float deltaX, deltaY;
	private const int MULTIPLIER = 100;
	[SerializeField]private bool clampRotationAngle = true;
	[SerializeField]private float rotationSpeed = 3.0f, maxRotationAngle = 80.0f;
	[SerializeField]private enum RotationDirection {X, Y, XY};
	[SerializeField]private RotationDirection rotationAxis = RotationDirection.X;

	// Use this for initialization
	void Start ()
	{
		cameraPosition = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float rSpd = rotationSpeed * Time.deltaTime * MULTIPLIER;
		deltaX += Input.GetAxis("Mouse X") * rSpd;
		deltaY -= Input.GetAxis("Mouse Y") * rSpd;
		if (clampRotationAngle) deltaY = Mathf.Clamp(deltaY, -maxRotationAngle, maxRotationAngle);
		
		switch (rotationAxis)
		{
			case RotationDirection.X:
			cameraPosition.localEulerAngles = new Vector3(0.0f, deltaX, 0.0f);
			break;
			case RotationDirection.Y:
			cameraPosition.localEulerAngles = new Vector3(deltaY, 0.0f, 0.0f);
			break;
			case RotationDirection.XY:
			cameraPosition.localEulerAngles = new Vector3(deltaY, deltaX, 0.0f);
			break;
		}
	}
}

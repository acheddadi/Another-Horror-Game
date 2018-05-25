using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform cameraPosition;
	private float deltaX, deltaY;
	[SerializeField]private bool clampRotationAngle = true;
	[SerializeField]private float rotationSpeed = 3.0f, maxRotationAngle = 80.0f;
	[SerializeField]private enum RotationDirection {X, Y, XY, NONE};
	[SerializeField]private RotationDirection rotationAxis = RotationDirection.X;
    private bool fps = false, dampedY = false;
    private float dampYTimer;
    private float velocity = 0.0f, smoothTime = 0.1f;

	// Use this for initialization
	void Start ()
	{
		cameraPosition = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!GameController.gameIsPaused)
        {
            float rSpd = rotationSpeed;
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
                case RotationDirection.NONE:
                    if (fps)
                    {
                        if (!dampedY)
                        {
                            cameraPosition.localEulerAngles = new Vector3(Mathf.SmoothDampAngle(cameraPosition.localEulerAngles.x, deltaY, ref velocity, smoothTime), 0.0f, 0.0f);
                            dampYTimer += Time.deltaTime;
                            if (dampYTimer > smoothTime * 2) dampedY = true;
                        }
                        else
                        {
                            cameraPosition.localEulerAngles = new Vector3(deltaY, 0.0f, 0.0f);
                            dampYTimer = 0.0f;
                        }
                        if (!fps && dampedY) dampedY = false;
                    }
                    else cameraPosition.localEulerAngles = new Vector3(Mathf.SmoothDampAngle(cameraPosition.localEulerAngles.x, 0.0f, ref velocity, smoothTime), 0.0f, 0.0f);
                    break;
            }
        }
	}

    public void FPS(bool f)
    {
        fps = f;
    }
}

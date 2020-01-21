using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunningCamera : MonoBehaviour
{
	public Transform cam;

	public float speedRotation = 55;
	public float maxRotation = 10; 
	public float radius = 0.75f;
	float angleZ;
	CharacterController characterController;
	private void Start()
	{
		characterController = GetComponent<CharacterController>(); 
	}

	private void Update()
	{
		RotationCamera(); 
	}

	void RotationCamera()
	{
		RaycastHit hit;
		if (!characterController.isGrounded)
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, radius))
			{
				angleZ += speedRotation * Time.deltaTime;
			}
			else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, radius))
			{
				angleZ -= speedRotation * Time.deltaTime;
			}
			else
			{
				angleZ = Mathf.MoveTowards(angleZ, 0, speedRotation * Time.deltaTime);
			}
		}
		else
		{
			angleZ = Mathf.MoveTowards(angleZ, 0, speedRotation * Time.deltaTime);
		}
		angleZ = Mathf.Clamp(angleZ, -maxRotation, maxRotation);

		Vector3 camHolderRotation = cam.parent.transform.eulerAngles;

		cam.eulerAngles = new Vector3(camHolderRotation.x, camHolderRotation.y, angleZ);
	}
}

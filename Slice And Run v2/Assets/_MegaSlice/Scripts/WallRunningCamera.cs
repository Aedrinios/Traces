using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunningCamera : MonoBehaviour
{
	public Transform cam;

	public float speedRotation = 55;
	public float maxRotation = 10; 
	public float radius = 0.75f;
	public float delayDectection = 0.05f; 

	float angleZ;
	Vector3 jumpDirection; 
	CharacterController characterController;
	bool onGround;

	float chrono = 0; 


	private void Start()
	{
		characterController = GetComponent<CharacterController>(); 
	}

	private void Update()
	{
		CheckOnGround(); 
		RotationCamera();
	}

	void RotationCamera()
	{
		RaycastHit hit;
		if (!onGround)
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

	void CheckOnGround()
	{
		chrono += Time.deltaTime; 
		if (chrono >= delayDectection)
		{
			if (characterController.isGrounded)
			{
				onGround = true;
			}
			else
			{
				onGround = false;
			}
			chrono = 0; 
		}
	}

}

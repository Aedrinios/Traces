using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunningCamera : MonoBehaviour
{
	//le wallRunning est en fait lié à la présence d'un mur ou non
	// Ne s'active que quand on est en l'air, mais pour les tests je fais le faire à terre aussi.

	public Transform cam;
	public float angleZ; 
	public float radius = 0.75f;
	float way = 0;


	private void Update()
	{
		cam.eulerAngles = new Vector3(cam.eulerAngles.x, cam.eulerAngles.y, angleZ); 
	}

	void IdentifyWallNear()
	{
		angleZ = 0;
		RaycastHit hitRight;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitRight, radius))
		{
			angleZ = 10;
		}
		RaycastHit hitLeft;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitLeft, radius))
		{
			angleZ = -10;
		}
	}
}

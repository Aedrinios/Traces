using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallJump : MonoBehaviour
{
	//pas le bon script pour la rotation de la caméra lors des wall running

	[Range(-1,1)] public float way = 0;
	public float speedRotate = 150;
	public float delay = 0.5f;

	public float radius = 1; 

	bool isDuring = false;

	public void RotateCamera()
	{
		IdentifyWallNear(); 
		if (!isDuring) StartCoroutine(RotateCameraCor()); 
	}

	void IdentifyWallNear()
	{
		way = 0; 
		RaycastHit hitRight; 
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitRight, radius))
		{
			way = 1; 
		}
		RaycastHit hitLeft;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitLeft, radius))
		{
			way = -1;
		}
	}

	IEnumerator RotateCameraCor()
	{
		bool playing = true;
		float chrono = 0;
		int step = 1;
		isDuring = true;

		float smoothSpeed = 0; 

		while (playing)
		{
			chrono += Time.deltaTime;

			smoothSpeed = Mathf.Lerp(0, speedRotate, chrono / (delay*0.5f));
			Debug.Log(smoothSpeed); 

			transform.Rotate(new Vector3(0, 0, 1) * step * way * smoothSpeed * Time.deltaTime);

			if (chrono >= delay/2)
			{
				step = -1; 
			}
			if (chrono >= delay)
			{
				playing = false; 
			}

			yield return null; 
		}

		transform.rotation = transform.parent.transform.rotation;
		isDuring = false; 
	}
}

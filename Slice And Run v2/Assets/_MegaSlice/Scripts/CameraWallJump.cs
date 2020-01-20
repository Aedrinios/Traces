using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallJump : MonoBehaviour
{
	public float speedRotate = 150;
	public float delay = 0.5f;

	bool isDuring = false;
	int way = 1; 

	public void RotateCamera()
	{
		if (!isDuring) StartCoroutine(RotateCameraCor()); 
	}

	IEnumerator RotateCameraCor()
	{
		bool playing = true;
		float chrono = 0;
		int step = 1;
		isDuring = true;

		while (playing)
		{
			chrono += Time.deltaTime; 
			transform.Rotate(new Vector3(0, 0, 1) * step * way * speedRotate * Time.deltaTime);

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

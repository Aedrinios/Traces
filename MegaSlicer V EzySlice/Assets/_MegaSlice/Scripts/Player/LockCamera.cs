using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour
{
	FPS_Controller fps;
	[TextArea] public string Notes = "!!! EN TEST !!! en cochant, on désactive la mécanique de lock";

	private void Start()
	{
		fps = GetComponent<FPS_Controller>();
		fps.canMoveCamera = true;
	}

	private void Update()
	{
		if (Input.GetButton("Fire2"))
		{
			fps.canMoveCamera = false; 
		}
		if (Input.GetButtonUp("Fire2"))
		{
			fps.canMoveCamera = true;
		}
	}

}

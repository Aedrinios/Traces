using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CursorChangeColor : MonoBehaviour
{
	public Color colorTrigger = Color.red; 
	public float sizeDetection = 10f;
	Image img; 
	Transform camTransform;


	Color originalColor; 

	private void Start()
	{
		camTransform = Camera.main.transform;
		img = GetComponent<Image>();
		originalColor = img.color; 
	}

	private void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(camTransform.position, camTransform.TransformDirection(Vector3.forward), out hit, sizeDetection, gameObject.layer))
		{
			if (hit.collider.gameObject.tag == "Sliceable" || hit.collider.gameObject.tag == "HeartCube")
			{
				img.color = colorTrigger;
			}
			else
			{
				img.color = originalColor; 
			}
		}
	}

}

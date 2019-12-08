using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastequeCanon : MonoBehaviour
{
	[Header("Setting Shot")]
	public GameObject pasteque;
	public Vector3 axeShot;
	public float forceShot = 80;
	public float scalePasteque = 2;

	[Header("Setting Event")]
	public GameObject activableObject;
	public float delayBetweenShot = 0.2f;
	public int numberPasteque = 16;
	[HideInInspector] public bool trigger = false;

	private void Update()
	{
		if(activableObject == null && !trigger)
		{
			StartCoroutine(EventShotPasteque());
			trigger = true;
		}
	}

	IEnumerator EventShotPasteque()
	{
		bool playing = true;
		float chrono = delayBetweenShot;
		int counter = 0; 

		while (playing)
		{
			chrono += Time.deltaTime; 

			if(chrono >= delayBetweenShot)
			{
				ShotPasteque();
				chrono = 0;
				counter++; 
			} 

			if (counter >= numberPasteque)
			{
				playing = false; 
			}
			yield return null;
		}	 
	}

	void ShotPasteque()
	{
		GameObject newPasqteque = Instantiate(pasteque, transform.position, transform.rotation) as GameObject;
		newPasqteque.transform.localScale = Vector3.one * scalePasteque; 
		Rigidbody rb = newPasqteque.GetComponent<Rigidbody>();
		rb.AddForce(forceShot * 10 * axeShot.normalized); 
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPasteque : MonoBehaviour
{
	public GameObject pasteque;
	public bool trigger = false; 

	public int numberPasteque = 40;
	public int variationNumber = 10; 
	
	public float range = 100f;
	public float height = 50f;
	public float variationHeight = 10f;

	public float minScale = 0.5f;
	public float maxScale = 4f;

	Vector3 originalPosition; 

	private void Start()
	{
		originalPosition = transform.position; 
	}

	private void Update()
	{
		if (trigger)
		{
			PastequeRain();
			trigger = false; 
		}
	}

	void PastequeRain()
	{
		int random_number = numberPasteque + Random.Range(-variationNumber, variationNumber);
		for (int i = 0; i < random_number; i++)
		{
			float random_X = originalPosition.x + Random.Range(-range, range);
			float random_Y = height + Random.Range(-variationHeight, variationHeight);
			float random_Z = originalPosition.z + Random.Range(-range, range);
			Vector3 positionSpawn = new Vector3(random_X, random_Y, random_Z);
			GameObject newPasteque = Instantiate(pasteque, positionSpawn, transform.rotation) as GameObject;
			float random_scale = Random.Range(minScale, maxScale);

			newPasteque.transform.localScale = pasteque.transform.localScale * random_scale;
		}

	}

	private void OnEnable()
	{
		EventHandler.StartPastequeRain += PastequeRain; 
	}

	private void OnDisable()
	{
		EventHandler.StartPastequeRain -= PastequeRain;
	}

}

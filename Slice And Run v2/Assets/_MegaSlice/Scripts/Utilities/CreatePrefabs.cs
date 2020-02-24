using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefabs : MonoBehaviour
{
	public GameObject prefabToSpawn; 

	public void SpawnPrefabs()
	{
		Instantiate(prefabToSpawn, transform.position, transform.rotation); 
	}
}

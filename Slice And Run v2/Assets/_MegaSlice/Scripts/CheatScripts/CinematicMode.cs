using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMode : MonoBehaviour
{
	public GameObject CamCinematic;
	public bool camIsOn = false; 

	List<GameObject> objectsUI = new List<GameObject>();

	private void Start()
	{
		if (!camIsOn) CamCinematic.SetActive(false); 
	}

	private void Update()
	{		
			if (Input.GetKeyDown(KeyCode.C))
			{
				//CinematicModeStart(); 
			}		
	}

	void CinematicModeStart()
	{
		//CamCinematic = GetComponentInChildren<Camera>().gameObject;
		CamCinematic.SetActive(true);

		GameObject player = GameObject.FindWithTag("Player");
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation; 

		GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
		for (int i = 0; i < allGameObjects.Length; i++)
		{
			if (allGameObjects[i].layer == 5)
			{
				objectsUI.Add(allGameObjects[i]); 
			}
		}
		for (int i = 0; i < objectsUI.Count; i++)
		{
			objectsUI[i].SetActive(false); 
		}

		FPS_Controller fps = FindObjectOfType<FPS_Controller>();
		fps.canPlay = false;

		PlayerAttack attack = FindObjectOfType<PlayerAttack>();
		attack.enabled = false; 
	}



}

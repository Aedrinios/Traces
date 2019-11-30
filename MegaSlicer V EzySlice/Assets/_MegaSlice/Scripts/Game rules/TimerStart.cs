using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStart : MonoBehaviour
{
	public GameObject player;
	public Text textTimer; 
	public float delayStart = 3f;
	public float speedTimer = 1.5f; 
	float chrono = 0;

	bool playing = false; 

	private void Start()
	{
		chrono = 0;
		playing = false; 
		textTimer.enabled = true;
		textTimer.text = ""; 
		player.GetComponent<FPS_Controller>().enabled = false;
		player.GetComponent<PlayerAttack>().enabled = false;
		player.GetComponent<MouseControl>().enabled = false;
	}

	private void Update()
	{
		if (Input.anyKeyDown) playing = true; 
		if (playing) chrono += Time.deltaTime * speedTimer; 
		
		if (chrono >= delayStart + 1f)
		{
			BeginGame(); 
		}
		else if (chrono >= delayStart)
		{
			textTimer.text = "CUT !!!"; 
		}
		else if (chrono != 0)
		{
			string chronoText = Mathf.Floor(chrono).ToString();
			textTimer.text = chronoText; 
		}

	}

	void BeginGame()
	{
		EventHandler.BeginGame?.Invoke(); 
		textTimer.enabled = false;
		player.GetComponent<FPS_Controller>().enabled = true;
		player.GetComponent<PlayerAttack>().enabled = true;
		player.GetComponent<MouseControl>().enabled = true;
		this.enabled = false; 
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStart : MonoBehaviour
{
	public GameObject player;
	public Text startTimerText; 
	public float delayStart = 3f;
	public float speedTimer = 1.5f; 
	float chrono = 0;

	bool playing = false; 
	   	 
	private void Start()
	{
		FindObject(); 
		chrono = 0;
		playing = false;
		startTimerText.enabled = true;
		startTimerText.text = ""; 
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
			startTimerText.text = "CUT !!!"; 
		}
		else if (chrono != 0)
		{
			string chronoText = Mathf.Floor(chrono).ToString();
			startTimerText.text = chronoText; 
		}

	}

	void BeginGame()
	{
		EventHandler.BeginTimer?.Invoke();
		startTimerText.enabled = false;
		player.GetComponent<FPS_Controller>().enabled = true;
		player.GetComponent<PlayerAttack>().enabled = true;
		player.GetComponent<MouseControl>().enabled = true;
		this.enabled = false; 
	}

	void FindObject()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			Debug.Log("il faut attacher le joueur au GameController"); 
		}
		if (startTimerText == null)
		{
			startTimerText = GameObject.Find("StartTimerText").GetComponent<Text>(); 
			Debug.Log("il faut attacher le StartTimerText au GameController");
		}

	}
}

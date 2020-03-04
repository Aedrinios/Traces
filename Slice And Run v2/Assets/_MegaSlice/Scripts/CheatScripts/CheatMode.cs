using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMode : MonoBehaviour
{
	public KeyCode cheatInput = KeyCode.F;
	public static bool cheatModeIsOn = false; 

    void Update()
    {
        if (Input.GetKeyDown(cheatInput))
		{
			if (!cheatModeIsOn)
			{
				ChronoSystem lifeTimerManager = GameObject.FindObjectOfType<ChronoSystem>();
				if (lifeTimerManager != null) lifeTimerManager.enabled = false;
				cheatModeIsOn = true;
				Debug.Log("Cheat Mode is on"); 
			}
			else
			{
				ChronoSystem lifeTimerManager = GameObject.FindObjectOfType<ChronoSystem>();
				if (lifeTimerManager != null) lifeTimerManager.enabled = true;
				cheatModeIsOn = false;
				Debug.Log("Cheat Mode is off");
			}
		}
    }
}

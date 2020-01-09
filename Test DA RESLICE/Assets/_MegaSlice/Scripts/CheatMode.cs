using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMode : MonoBehaviour
{
	public KeyCode cheatInput = KeyCode.F;
	public bool cheatModeIsOn = false; 

    void Update()
    {
        if (Input.GetKeyDown(cheatInput))
		{
			if (!cheatModeIsOn)
			{
				LifeTimerManager lifeTimerManager = GameObject.FindObjectOfType<LifeTimerManager>();
				lifeTimerManager.enabled = false;
				cheatModeIsOn = true;
				Debug.Log("Cheat Mode is on"); 
			}
			else
			{
				LifeTimerManager lifeTimerManager = GameObject.FindObjectOfType<LifeTimerManager>();
				lifeTimerManager.enabled = true;
				cheatModeIsOn = false;
				Debug.Log("Cheat Mode is off");
			}
		}
    }
}

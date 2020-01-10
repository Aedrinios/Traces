using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float forcePushCut = 80; 

	public static float forcePushCutStc;

	private void Awake()
	{
		forcePushCutStc = forcePushCut; 
	}

    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

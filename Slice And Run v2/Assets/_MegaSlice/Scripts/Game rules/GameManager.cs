using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    #endregion

    public Transform playerPosition;

    public float forcePushCut = 80;
	public static float forcePushCutStc;

	private void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
		forcePushCutStc = forcePushCut;
        playerPosition = GameObject.Find("StartSpawner").transform;
    }
}

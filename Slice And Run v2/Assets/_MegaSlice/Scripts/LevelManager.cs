using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public delegate void OnLevelComplete();
    public static OnLevelComplete onLevelComplete;

    public delegate void OnLevelFailed();
    public static OnLevelFailed onLevelFailed;

    private PlayerManager playerManager;
    private GameObject scoreScreen;
    private GameObject failedScreen;
    private GameObject playerInterface;
    private FPS_Controller fpsController;
    private PlayerAttack attackController;

    public static bool isLevelEnding = false; 

    private int sceneIndex;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        onLevelComplete += ShowScoreScreen;
        onLevelFailed += ShowFailedScreen;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        onLevelComplete -= ShowScoreScreen;
        onLevelFailed -= ShowFailedScreen;
    }

    private void Start()
    {
        isLevelEnding = false; 
        fpsController = FindObjectOfType<FPS_Controller>();
        attackController = FindObjectOfType<PlayerAttack>();
    }

    void ShowScoreScreen()
    {
        isLevelEnding = true; 
        ProgressionManager.UnlockLevel(SceneManager.GetActiveScene().buildIndex);        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        scoreScreen.SetActive(true);
        SaveScore();
        DeactivatePlayer();
    }

    private void ShowFailedScreen()
    {
        isLevelEnding = true; 
        failedScreen.SetActive(true);
        GameManager.Instance.hasFailed = true;
        DeactivatePlayer();
    }

    private void DeactivatePlayer()
    {
        fpsController.canPlay = false;
        fpsController.canMoveCamera = false;
        fpsController.canJump = false;
        attackController.enabled = false;
        playerInterface.SetActive(false);   
    }

    private void SaveScore()
    {
        scoreScreen.transform.Find("ScoreText").GetComponent<TMP_Text>().text = LifeTimerManager.lifeTimer.ToString("F2");
        playerManager.SaveScore(sceneIndex - 1, LifeTimerManager.lifeTimer);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLevelEnding = false; 
        sceneIndex = scene.buildIndex;
        if (sceneIndex > 0)
        {
            playerManager = FindObjectOfType<PlayerManager>();
            playerInterface = GameObject.Find("PlayerInterface");
            scoreScreen = GameObject.Find("Canvas").transform.Find("ScoreScreen").gameObject;
            failedScreen = GameObject.Find("Canvas").transform.Find("FailedScreen").gameObject;
            scoreScreen.SetActive(false);
            failedScreen.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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

    private HighscoreTable leaderboard;

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
        ProgressionManager.UnlockLevel(sceneIndex);        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        scoreScreen.SetActive(true);
        SaveScore();
        leaderboard.SortPlayerList(sceneIndex - 1);
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
        TimeSpan timeSpan = TimeSpan.FromSeconds(LifeTimerManager.lifeTimer);
        scoreScreen.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text += string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        scoreScreen.transform.Find("ScoreText").GetComponent<TranslateText>().frenchText += string.Format(" {0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

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
            leaderboard = scoreScreen.GetComponent<HighscoreTable>();
            failedScreen = GameObject.Find("Canvas").transform.Find("FailedScreen").gameObject;
            scoreScreen.SetActive(false);
            failedScreen.SetActive(false);
        }
    }
}

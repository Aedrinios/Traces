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
    private GameObject newRecord;
    private FPS_Controller fpsController;
    private PlayerAttack attackController;
    private HighscoreTable leaderboard;

    public static bool isLevelEnding = false;
    public static bool hasBeatTimer = true;

    private int sceneIndex;

    private void OnEnable()
    {
        //CheckProgressionManager(); 
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            onLevelComplete += ShowScoreScreen;
            onLevelFailed += ShowFailedScreen;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            onLevelComplete -= ShowScoreScreen;
            onLevelFailed -= ShowFailedScreen;
        }
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
        scoreScreen.SetActive(true);
        SaveScore();
        leaderboard.SortPlayerList(sceneIndex - 1);

        if (hasBeatTimer)
        {
            ProgressionManager.UnlockLevel(sceneIndex);
        }
        else
        {
            Debug.Log("is level unlock ? : " + ProgressionManager.listLevel[sceneIndex]);
            if(!ProgressionManager.listLevel[sceneIndex])
                scoreScreen.transform.Find("Buttons").Find("NextButton").gameObject.SetActive(false);

            scoreScreen.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Too slow...";
            scoreScreen.transform.Find("ScoreText").GetComponent<TranslateText>().frenchText = "Trop lent...";
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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
        TimeSpan timeSpan = TimeSpan.FromSeconds(ChronoSystem.chronoStc);
        scoreScreen.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text += string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        scoreScreen.transform.Find("ScoreText").GetComponent<TranslateText>().frenchText += string.Format(" {0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        RankSystem rankSystem = FindObjectOfType<RankSystem>();
        rankSystem.RankPlayer();
        if (playerManager.SaveScore(sceneIndex - 1, ChronoSystem.chronoStc, rankSystem.rank)) {
           newRecord.SetActive(true);
        };
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasBeatTimer = true;
        isLevelEnding = false; 
        sceneIndex = scene.buildIndex;
        playerManager = FindObjectOfType<PlayerManager>();
        if(sceneIndex == 0)
        {
            playerManager.FindInputField();
        }
        else if (sceneIndex > 0)
        {
            playerInterface = GameObject.Find("PlayerInterface");
            scoreScreen = GameObject.Find("Canvas").transform.Find("ScoreScreen").gameObject;
            leaderboard = scoreScreen.GetComponent<HighscoreTable>();
            newRecord = scoreScreen.transform.Find("NewRecord").gameObject;
            failedScreen = GameObject.Find("Canvas").transform.Find("FailedScreen").gameObject;
            scoreScreen.SetActive(false);
            failedScreen.SetActive(false);
        }
    }

    void CheckProgressionManager()
    {
        GameObject dataManager = GameObject.FindWithTag("DataManager");

        if (dataManager == null)
        {
            GameObject go = new GameObject("DataManagerNotGood");
            go.tag = "DataManager"; 
            go.AddComponent<PutSceneDontDestroy>();
            go.AddComponent<ProgressionManager>();
            go.AddComponent<PlayerManager>();
            go.AddComponent<UnlockLevels>();
 
        }
    }
}

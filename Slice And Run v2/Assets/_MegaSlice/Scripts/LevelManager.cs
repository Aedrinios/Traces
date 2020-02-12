using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public delegate void OnLevelComplete();
    public static OnLevelComplete onLevelComplete;

    public delegate void OnLevelFailed();
    public static OnLevelFailed onLevelFailed;

    private GameObject scoreScreen;
    private GameObject failedScreen;
    private GameObject playerInterface;
    private FPS_Controller fpsController;
    private PlayerAttack attackController;

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
        fpsController = FindObjectOfType<FPS_Controller>();
        attackController = FindObjectOfType<PlayerAttack>();
    }

    void ShowScoreScreen()
    {
        ProgressionManager.UnlockLevel(SceneManager.GetActiveScene().buildIndex);
 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        scoreScreen.SetActive(true);

        DeactivatePlayer();
    }

    private void ShowFailedScreen()
    {
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex > 0)
        {
            playerInterface = GameObject.Find("PlayerInterface");
            scoreScreen = GameObject.Find("Canvas").transform.Find("ScoreScreen").gameObject;
            failedScreen = GameObject.Find("Canvas").transform.Find("FailedScreen").gameObject;
        }
    }
}

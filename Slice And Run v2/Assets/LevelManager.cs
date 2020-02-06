using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public delegate void OnLevelComplete();
    public static OnLevelComplete onLevelComplete;

    private GameObject scoreScreen;
    private GameObject playerInterface;
    private FPS_Controller fpsController;
    private PlayerAttack attackController;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        onLevelComplete += ShowScoreScreen;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        onLevelComplete -= ShowScoreScreen;
    }

    private void Start()
    {
        fpsController = FindObjectOfType<FPS_Controller>();
        attackController = FindObjectOfType<PlayerAttack>();
    }

    void ShowScoreScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        fpsController.canPlay = false;
        fpsController.canMoveCamera = false;
        fpsController.canJump = false;
        attackController.enabled = false;
        playerInterface.SetActive(false);
        scoreScreen.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex > 0)
        {
            playerInterface = GameObject.Find("PlayerInterface");
            scoreScreen = GameObject.Find("Canvas").transform.Find("scoreScreen").gameObject;
        }
    }
}

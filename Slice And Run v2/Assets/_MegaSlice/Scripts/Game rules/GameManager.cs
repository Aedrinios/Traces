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

    private GameObject loadingScreen;
    private Image loadingBar;

    public float forcePushCut = 80;
	public static float forcePushCutStc;

    [HideInInspector] public bool hasFailed;

    private void Awake()
    {
        forcePushCutStc = forcePushCut;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.anyKeyDown && hasFailed)
        {
            hasFailed = false;
            RetryLevel();
        }
    }

    public void LaunchLevel(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    public void RetryLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LaunchNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("yes ok");
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            StartCoroutine(LoadScene(0));
        }
    }

    private IEnumerator LoadScene(int id)
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        loadingBar.fillAmount = 0f;

        while (!asyncLoad.isDone)
        {
            loadingBar.fillAmount = asyncLoad.progress;
            yield return null;
        }

        loadingScreen.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadingScreen = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        loadingBar = loadingScreen.transform.GetChild(2).gameObject.GetComponent<Image>();
        if (scene.buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

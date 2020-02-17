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
    private GameObject levelScreen;
    private Image loadingBar;

    public float forcePushCut = 80;
	public static float forcePushCutStc;

    [HideInInspector] public bool hasFailed;
    public static bool levelScreenOpened;

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
        levelScreen = GameObject.Find("Canvas").transform.Find("LevelScreen").gameObject;
        loadingBar = loadingScreen.transform.GetChild(2).gameObject.GetComponent<Image>();
        if (scene.buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnMenuLoaded();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnMenuLoaded()
    {
        if (levelScreenOpened)
        {
            levelScreen.SetActive(true);
            LevelButton[] allButtons = GameObject.Find("Canvas").GetComponentsInChildren<LevelButton>(true);
            foreach (LevelButton button in allButtons)
            {
                Debug.Log("i'm here");
                Debug.Log("button id " + button.id);
                Debug.Log("is it unlock ?" + ProgressionManager.listLevel[button.id]);

                if (ProgressionManager.listLevel[button.id])
                {
                    Debug.Log("then i'm here");
                    button.onLevelUnlocked?.Invoke();
                }
            }
        }
    }

    public void SetLevelScreenToLoaded()
    {
        levelScreenOpened = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

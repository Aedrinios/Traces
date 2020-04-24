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

    [HideInInspector] public bool hasFailed;
    public static bool levelScreenOpened;

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
        float chrono = 0; 

        while(chrono <= 0.25f)
        {
            chrono += Time.deltaTime; 
            yield return null;
        }


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
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        if (scene.buildIndex == 0)
        {
            levelScreen = GameObject.Find("Canvas").transform.Find("LevelScreen").gameObject;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnMenuLoaded();
        }
        else
        {
            Cursor.visible = false;
            if(FindObjectOfType<MouseControl>().collotRotation)
                Cursor.lockState = CursorLockMode.Confined;
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
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
                if (ProgressionManager.listLevel[button.id])
                {
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

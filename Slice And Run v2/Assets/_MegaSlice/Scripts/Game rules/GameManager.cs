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

    public Transform playerPosition;

    public float forcePushCut = 80;
	public static float forcePushCutStc;

    private void Awake()
    {
      //  DontDestroyOnLoad(this.gameObject);
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

    public void LaunchLevel(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
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

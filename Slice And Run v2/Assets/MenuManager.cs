using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingBar;

    public void LaunchGame()
    {
        StartCoroutine(LoadScene(1));
    }

    private IEnumerator LoadScene(int id)
    {
        loadingPanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        loadingBar.fillAmount = 0f;

        while (!asyncLoad.isDone)
        {
            loadingBar.fillAmount = asyncLoad.progress;
            yield return null;
        }

        loadingPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

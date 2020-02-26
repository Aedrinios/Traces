using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public string name;
    public float[] scoreList;

    public TMP_InputField inputName;
    public GameObject changeNameButton;

    public void Awake()
    {
        Debug.Log("hello");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("ah yes");
            inputName = FindObjectOfType<TMP_InputField>();
            Debug.Log(FindObjectOfType<TMP_InputField>());
            inputName.onEndEdit.AddListener(delegate { SaveName(); });
        }
        else
        {
            Debug.Log("am i here?");
            inputName.onEndEdit.RemoveAllListeners();
        }

        SaveSystem.Init();
        PlayerData data = SaveSystem.LoadGame();
        scoreList = new float[SceneManager.sceneCountInBuildSettings - 1];

        if (data != null)
        {
            name = data.name;
            scoreList = data.scoreList;
            inputName.gameObject.SetActive(false);
            changeNameButton.SetActive(true);
        }
    }

    private void Start()
    {
        
    }

    public void SaveName()
    {
        Debug.Log("should be here");
        name = inputName.text;
        if(inputName.text.Length > 0)
        {
            if (SaveSystem.CreateFile(this))
            {
                Debug.Log("or here");
                inputName.gameObject.SetActive(false);
                changeNameButton.SetActive(true);
            }
            else
            {
                inputName.text = "";
                inputName.placeholder.GetComponent<TextMeshProUGUI>().text = "Nom déjà pris";
            }

        }
    }

    public void SaveScore(int levelIndex, float score)
    {
        if (scoreList[levelIndex] < score)
        {
            scoreList[levelIndex] = score;
            SaveSystem.SavePlayer(this);
        }
    }
}

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
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindInputField();
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

    public void FindInputField()
    {
        inputName = GameObject.Find("MenuScreen").transform.Find("Name").gameObject.GetComponent<TMP_InputField>();
        changeNameButton = GameObject.Find("MenuScreen").transform.Find("ChangeName").gameObject;
        inputName.onEndEdit.AddListener(delegate { SaveName(); });
    }

    public void SaveName()
    {
        name = inputName.text;
        if(inputName.text.Length > 0)
        {
            if (SaveSystem.CreateFile(this))
            {
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
        if (scoreList[levelIndex] > score || scoreList[levelIndex] == 0)
        {
            scoreList[levelIndex] = score;
            SaveSystem.SavePlayer(this);
        }
    }
}

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
    public string[] rankList;

    public TMP_InputField inputName;
    public GameObject changeNameButton;

    public RankDisplay rankDisplay;

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
            rankList = data.rankList;
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
            SaveSystem.CreateFile(this);
            inputName.gameObject.SetActive(false);
            changeNameButton.SetActive(true);
        }
    }

    public bool SaveScore(int levelIndex, float score, string rank)
    {
        if (scoreList[levelIndex] > score || scoreList[levelIndex] == 0)
        {
            scoreList[levelIndex] = score;
            rankList[levelIndex] = rank;
            SaveSystem.SavePlayer(this);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckIfLevelAreUnlockable()
    {
        for (int i = 0; i < scoreList.Length - 1; i++)
        {
            if (scoreList[i] > 0)
            {
                FindObjectOfType<UnlockLevels>().UnlockOneLevel(i + 1);
                if (i + 1 < scoreList.Length)
                {
                    FindObjectOfType<UnlockLevels>().UnlockOneLevel(i + 2);
                }
            }
        }
    }
}

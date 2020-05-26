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
    public GameObject displayName;
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
            displayName.SetActive(true);
            changeNameButton.SetActive(true);
        }
    }

    public void FindInputField()
    {
        GameObject menuScreen = GameObject.Find("MenuScreen");
        inputName = menuScreen.transform.Find("Name").gameObject.GetComponent<TMP_InputField>();
        displayName = menuScreen.transform.Find("CurrentName").gameObject;
        changeNameButton = GameObject.Find("MenuScreen").transform.Find("LogOut").gameObject;
        inputName.onEndEdit.AddListener(delegate { SaveName(); });
    }

    public void SaveName()
    {
        name = inputName.text;
        if(inputName.text.Length > 0)
        {
            SaveSystem.CreateFile(this);
            inputName.gameObject.SetActive(false);
            displayName.SetActive(true);
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
        UnlockLevels unlockLevels = FindObjectOfType<UnlockLevels>();
        int lastUnlock = -1;
        for (int i = 0; i < scoreList.Length; i++)
        {
            if (!(rankList[i] == "A" || rankList[i] == "B" || rankList[i] == "C"))
            {
                unlockLevels.LockOneLevel(i);
            }
            else
            {
                unlockLevels.UnlockOneLevel(i + 1);
                lastUnlock = i;
            }
        }
        unlockLevels.UnlockOneLevel(0);
        Debug.Log("last unlokc : " + lastUnlock);
        if(lastUnlock < scoreList.Length - 1 && lastUnlock > -1)
        {
            unlockLevels.UnlockOneLevel(lastUnlock + 2);
        }
    }
}

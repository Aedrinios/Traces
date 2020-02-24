using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public string name;
    public float[] scoreList;

    public TMP_InputField inputName;

    public void Awake()
    {
        scoreList = new float[SceneManager.sceneCountInBuildSettings - 1];
    }

    public void Start() {     
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            inputName = FindObjectOfType<TMP_InputField>();
            inputName.onEndEdit.AddListener(delegate { SaveName(); });
        }
        else
        {
            inputName.onEndEdit.RemoveAllListeners();
        }
    }

    public void SaveName()
    {
        name = inputName.text;
        SaveSystem.SavePlayer(this);
    }

    public void SaveScore(int levelIndex, float score)
    {
        scoreList[levelIndex] = score;
    }
}

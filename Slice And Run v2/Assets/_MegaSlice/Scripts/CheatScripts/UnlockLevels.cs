using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevels : MonoBehaviour
{
    public KeyCode keyUnlock = KeyCode.U;
    public Transform menuLevel;

    private void Awake()
    {
        menuLevel = GameObject.Find("Canvas").transform.Find("LevelScreen").Find("Map").Find("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyUnlock))
        {
            UnlockAllLevels(); 
        }
    }

    public void UnlockOneLevel(int levelNumber)
    {
        ProgressionManager.UnlockLevel(levelNumber);
        menuLevel.Find("Level" + levelNumber).gameObject.GetComponent<LevelButton>().onLevelUnlocked?.Invoke();
        /*LevelButton[] allLevel = FindObjectsOfType<LevelButton>();
        
        for (int i = 0; i < allLevel.Length; i++)
        {
            Debug.Log("button id" + allLevel[i].id);
            Debug.Log("level id" + levelNumber);
            if (allLevel[i].id == levelNumber - 1)
            {
                Debug.Log("about to unlock");
                allLevel[i].UnlockLevel();
            }
        }*/
    }

    void UnlockAllLevels()
    {
        int lengthScenes = SceneManager.sceneCountInBuildSettings;

        for (int i = 1; i < lengthScenes; i++)
        {
            ProgressionManager.UnlockLevel(i);
        }

        LevelButton[] allLevel = FindObjectsOfType<LevelButton>();

        for (int i = 0; i < allLevel.Length; i++)
        {
            allLevel[i].UnlockLevel(); 
        }
    }
}

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
            //UnlockAllLevels(); 
        }
    }

    void UnlockAllLevels()
    {
        //int lengthScenes = SceneManager.sceneCountInBuildSettings;
        int lengthScenes = ProgressionManager.numberOfLevel;

        for (int i = 1; i < lengthScenes; i++)
        {
            ProgressionManager.UnlockLevel(i);
        }

        LevelButton[] allLevelButton = FindObjectsOfType<LevelButton>();

        for (int i = 0; i < allLevelButton.Length; i++)
        {
            if (allLevelButton[i].id <= ProgressionManager.numberOfLevel-1)
            {
                allLevelButton[i].UnlockLevel();
            }            
        }
    }

    public void UnlockOneLevel(int levelNumber)
    {
        if(levelNumber == 0)
        {
            ProgressionManager.UnlockLevel(levelNumber);
            menuLevel.Find("Level" + (levelNumber+1)).gameObject.GetComponent<LevelButton>().onLevelUnlocked?.Invoke();
        }
        else
        {
            ProgressionManager.UnlockLevel(levelNumber - 1);
            menuLevel.Find("Level" + levelNumber).gameObject.GetComponent<LevelButton>().onLevelUnlocked?.Invoke();
        }
    }


    public void LockOneLevel(int levelNumber)
    {
        Debug.Log("level number " + levelNumber);
        ProgressionManager.LockLevel(levelNumber);
        Debug.Log(menuLevel.Find("Level" + (levelNumber + 1)).gameObject.GetComponent<LevelButton>());
        menuLevel.Find("Level" + (levelNumber + 1)).gameObject.GetComponent<LevelButton>().onLevelLocked?.Invoke();
    }
}

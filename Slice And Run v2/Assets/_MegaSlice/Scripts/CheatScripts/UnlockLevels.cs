using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevels : MonoBehaviour
{
    public KeyCode keyUnlock = KeyCode.U;

    private void Update()
    {
        if (Input.GetKeyDown(keyUnlock))
        {
            UnlockAllLevels(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UnlockOneLevel(6); 
        }
    }

    void UnlockOneLevel(int levelNumber)
    {
        ProgressionManager.UnlockLevel(levelNumber);

        LevelButton[] allLevel = FindObjectsOfType<LevelButton>();

        for (int i = 0; i < allLevel.Length; i++)
        {
            if (allLevel[i].id == levelNumber - 1)
            {
                allLevel[i].UnlockLevel();
            }
        }
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

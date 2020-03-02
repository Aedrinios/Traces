using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevels : MonoBehaviour
{
    public KeyCode keyUnlock = KeyCode.U;

    private void Update()
    {
        if (CheatMode.cheatModeIsOn)
        {
            if (Input.GetKeyDown(keyUnlock))
            {
                UnlockAllLevels(); 
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

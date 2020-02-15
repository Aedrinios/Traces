using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    [HideInInspector] public int numberOfLevel;
    public static bool[] listLevel;

    private void OnEnable()
    {
        CalculateNumberLevel(); 
        if (listLevel == null)
        {
            listLevel = new bool[numberOfLevel];
            UnlockLevel(0);

            for (int i = 1; i < listLevel.Length; i++)
            {
                listLevel[i] = false;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void UnlockLevel(int i)
    {
       listLevel[i] = true;
    }

    void CalculateNumberLevel()
    {
        numberOfLevel = SceneManager.sceneCountInBuildSettings - 1;
    }
}

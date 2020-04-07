using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    private static ProgressionManager instance;

    public static int numberOfLevel;
    public static bool[] listLevel;

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);

        IsLoaded();
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
    }

    public static void UnlockLevel(int i)
    {
       listLevel[i] = true;
    }

    void IsLoaded()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CalculateNumberLevel()
    {
        numberOfLevel = SceneManager.sceneCountInBuildSettings - 1;
        Debug.Log("Il y a " + numberOfLevel + " niveaux jouables"); 
    }
}

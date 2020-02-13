using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour
{
    public int numberOfLevel;

    public static bool[] listLevel;

    private void OnEnable()
    {
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
}

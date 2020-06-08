using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLanguageSystem : MonoBehaviour
{
    void Start()
    {
        //This checks if your computer's operating system is in the French language
        if (Application.systemLanguage == SystemLanguage.French)
        {
            //Outputs into console that the system is French
            Debug.Log("This system is in French. ");
        }
        //Otherwise, if the system is English, output the message in the console
        else 
        {
            Debug.Log("This system is in English. ");
        }
    }
}

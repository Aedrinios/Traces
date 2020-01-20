using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CleanupMissingScriptsHelper
{
    [MenuItem("Edit/Cleanup Missing Scripts")]
    static void CleanupMissingScripts()
    {
        GameObject[] allGameobject = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < allGameobject.Length; i++)
        {
            Component[] allComponent = allGameobject[i].GetComponents<Component>();

            SerializedObject serializedObject = new SerializedObject(allGameobject[i]);
            var prop = serializedObject.FindProperty("m_Component");
            int r = 0; 

            for (int j = 0; j < allComponent.Length ; j++)
            {
                if (allComponent[j] == null)
                {
                    prop.DeleteArrayElementAtIndex(j - r);
                    r++; 
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        Debug.Log("Clean is done"); 
    }
}

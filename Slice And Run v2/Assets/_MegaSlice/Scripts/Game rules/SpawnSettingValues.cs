using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSettingValues : MonoBehaviour
{
    public GameObject settingValue; 
    

    void Start()
    {
        GameObject otherSetting = GameObject.FindGameObjectWithTag("SettingValues"); 
        if (otherSetting == null)
        {
            Instantiate(settingValue, transform.position, transform.rotation); 
        }
    }


}

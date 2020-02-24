using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float[] scoreList;

    public PlayerData(string name, float[] scoreList)
    {
        this.name = name;
        this.scoreList = scoreList;
    }
}

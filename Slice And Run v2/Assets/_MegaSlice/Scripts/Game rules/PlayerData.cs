using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float[] scoreList;
    public string[] rankList;

    public PlayerData(string name, float[] scoreList, string[] rankList)
    {
        this.name = name;
        this.scoreList = scoreList;
        this.rankList = rankList;
    }
}

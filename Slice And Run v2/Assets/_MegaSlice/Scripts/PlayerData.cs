using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float[] scoreList;

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetScore(int levelIndex, float score)
    {
        scoreList[levelIndex] = score;
    }
}

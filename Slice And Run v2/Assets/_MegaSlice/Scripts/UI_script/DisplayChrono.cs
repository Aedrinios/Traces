using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DisplayChrono : MonoBehaviour
{
    TextMeshProUGUI timeText;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>(); 
    }

    private void Update()
    {
        float currentTime = ChronoSystem.chronoStc; 
        timeText.text = currentTime.ToString("F2");
    }

    private void OnEnable()
    {
        LevelManager.onLevelComplete += SaveScore;
    }

    public void SaveScore()
    {
        GameObject.Find("Canvas").transform.Find("ScoreScreen").Find("ScoreText").GetComponent<TextMeshProUGUI>().text = timeText.text;
    }
}

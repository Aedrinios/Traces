using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    TextMeshProUGUI timeText;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float currentTime = ChronoSystem.timerStc;
        timeText.text = currentTime.ToString("F2");
    }
}

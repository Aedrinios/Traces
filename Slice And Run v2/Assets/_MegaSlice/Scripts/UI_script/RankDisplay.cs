using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankDisplay : MonoBehaviour
{
    string[] rank = { " ",  "C", "B", "A" };
    public static int valueRank = 0; 
    TextMeshProUGUI textMeshPro; 

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = rank[valueRank]; 
    }

    private void Update()
    {
        textMeshPro.text = rank[valueRank];
        //SetAutomaticRank(); 
    }

    void SetAutomaticRank()
    {
        if (ChronoSystem.timerStc != 0 && !ChronoSystem.playing)
        {
            float ratio = ChronoSystem.chronoStc / ChronoSystem.limitTimerStc;

            if (ratio > 1)
            {
                textMeshPro.text = rank[0];
            }
            else if (ratio <= 1 && ratio > 0.8f)
            {
                textMeshPro.text = rank[1];
            }
            else if (ratio <= 0.8f && ratio > 0.6f)
            {
                textMeshPro.text = rank[2];
            }
            else
            {
                textMeshPro.text = rank[rank.Length - 1];
            }
        }
    }
}

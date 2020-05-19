using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankDisplay : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    private RankSystem rankSystem;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rankSystem = FindObjectOfType<RankSystem>();
    }

    public void Update()
    {
        textMeshPro.text = rankSystem.rank;
        //SetAutomaticRank(); 
    }

  /*  void SetAutomaticRank()
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
    }*/
}

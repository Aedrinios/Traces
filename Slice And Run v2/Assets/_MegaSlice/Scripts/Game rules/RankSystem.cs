using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSystem : MonoBehaviour
{
    public float timeRank_A;
    public float timeRank_B;
    public float timeRank_C;

    [HideInInspector] public string rank;

    public void RankPlayer()
    {
        //Debug.Log(ChronoSystem.chronoStc); 
        Debug.Log("Ranking in progress");
        if (!ChronoSystem.playing)
        {

            if (ChronoSystem.chronoStc <= timeRank_A)
            {
                rank = "A"; 
            }
            if (ChronoSystem.chronoStc > timeRank_A && ChronoSystem.chronoStc <= timeRank_B)
            {
                rank = "B";
            }
            if (ChronoSystem.chronoStc > timeRank_B && ChronoSystem.chronoStc <= timeRank_C)
            {
                rank = "C";
            }
            if (ChronoSystem.chronoStc > timeRank_C)
            {
                rank = "";
            }
            Debug.Log("rank : "  + rank);
        }
    }

}

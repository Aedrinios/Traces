using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSystem : MonoBehaviour
{
    public float timeRank_A;
    public float timeRank_B;
    public float timeRank_C;

    int valueRank = 0;

    private void Update()
    {
        //Debug.Log(ChronoSystem.chronoStc); 

        if (!ChronoSystem.playing)
        {

            if (ChronoSystem.chronoStc <= timeRank_A)
            {
                valueRank = 3; 
            }
            if (ChronoSystem.chronoStc > timeRank_A && ChronoSystem.chronoStc <= timeRank_B)
            {
                valueRank = 2;
            }
            if (ChronoSystem.chronoStc > timeRank_B && ChronoSystem.chronoStc <= timeRank_C)
            {
                valueRank = 1;
            }
            if (ChronoSystem.chronoStc > timeRank_C)
            {
                valueRank = 0;
            }

            RankDisplay.valueRank = valueRank; 
        }
    }

}

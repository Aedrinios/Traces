using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Die()
    {
        BasicTools.RestartScene();
    }
}

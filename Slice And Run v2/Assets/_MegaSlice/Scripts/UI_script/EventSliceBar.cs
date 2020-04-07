using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSliceBar : MonoBehaviour
{
    ChronoSystem chronoSystem;

    public GameObject barUnsliced;
    public GameObject barSliced;
    public float delayStopAnim = 1; 

    bool isHappen = false; 

    private void Start()
    {
        chronoSystem = GameObject.FindObjectOfType<ChronoSystem>();
        barUnsliced.SetActive(true);
        barSliced.SetActive(false);
        isHappen = false;;
    }

    private void Update()
    {
        if (ChronoSystem.timerStc <= 0 && ChronoSystem.playing && !isHappen)
        {
            SliceTimeBar();
            isHappen = true;
            Invoke("StopSliceAnim", delayStopAnim); 
        }
    }

    public void SliceTimeBar()
    {
        barUnsliced.SetActive(false);
        barSliced.SetActive(true);
    }

    void StopSliceAnim()
    {
        barSliced.GetComponent<Animator>().enabled = false;
    }
}

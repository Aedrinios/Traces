using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTimer : MonoBehaviour
{
    private LifeTimerManager timer;

    private void Start()
    {
        timer = FindObjectOfType<LifeTimerManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventHandler.BeginTimer?.Invoke();
        }
    }
}

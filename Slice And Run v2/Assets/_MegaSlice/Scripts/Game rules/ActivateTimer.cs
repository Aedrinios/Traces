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


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<FPS_Controller>().enabled)
        {
            EventHandler.BeginTimer?.Invoke();
        }
    }
}

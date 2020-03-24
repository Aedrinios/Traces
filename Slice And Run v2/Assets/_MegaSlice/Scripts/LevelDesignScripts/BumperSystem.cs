using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSystem : MonoBehaviour
{
    public float forcePush = 100; 
    FPS_Controller playerController;

    bool isOn = true; 

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
        isOn = true; 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isOn)
        {
            if (collision.gameObject.tag == "Player")
            {
                PushBumper();
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (isOn)
        {
            if (collision.gameObject.tag == "Player")
            {
                PushBumper();
            }
        }
    }

    void PushBumper()
    {
        playerController.PushPlayer(transform.up * forcePush);
        
        isOn = false;
        Invoke("ReloadBumper", 0.06f);
    }

    void ReloadBumper()
    {
        isOn = true;
    }
}

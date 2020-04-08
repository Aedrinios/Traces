using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class BumperSystem : MonoBehaviour
{
    public float forcePush = 100;
    public float reloadTime = 0.07f; 
    //public float minForcePush = 10; 
    public UnityEvent TriggerBumper; 
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
        // donne un boost pour contrer la gravité
        if (playerController.velocity.y >= 8)
        {
            playerController.PushPlayer(transform.up * forcePush * 0.1f);
        }
        else if (playerController.velocity.magnitude <= 8)
        {
            float speedPlayer = playerController.velocity.magnitude;
            playerController.velocity = Vector3.zero;
            playerController.PushPlayer(transform.up * speedPlayer);
        }


        playerController.PushPlayer(transform.up * forcePush);
        TriggerBumper.Invoke(); 
        isOn = false;
        Invoke("ReloadBumper", reloadTime);


    }

    void ReloadBumper()
    {
        isOn = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class DetectLanding : MonoBehaviour
{
    public UnityEvent LandingEvent; 
    CharacterController characterController;
    float chrono = 0; 

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        if (!characterController.isGrounded)
        {
            chrono += Time.deltaTime;
        }
        else
        {
            if (chrono >= 0.5f) LandingEvent.Invoke(); 
            chrono = 0; 
        }
    }
}

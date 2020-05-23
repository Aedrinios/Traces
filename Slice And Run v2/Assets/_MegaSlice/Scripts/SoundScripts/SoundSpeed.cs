using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeed : MonoBehaviour
{
    public float speedTrigger = 10f;
    public float speedMax = 50f;

    public float speedImprove = 10f;
    public float speedDecrease = 10f; 

    bool decrease = false;
    float value; 



    FPS_Controller fps;
    FmodParameter fmodParameter; 

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) fps = player.GetComponent<FPS_Controller>();

        fmodParameter = GetComponent<FmodParameter>(); 
    }

    void Update()
    {
        if (fps != null)
        {
            if (fps.moveDir.magnitude >= speedTrigger) decrease = false;            
            else decrease = true;
            
            if (decrease)
            {
                value = Mathf.MoveTowards(value, 0, speedDecrease * Time.deltaTime); 
            }
            else
            {
                value = Mathf.MoveTowards(value, fps.moveDir.magnitude / speedMax, speedImprove * Time.deltaTime);
            }

            value = Mathf.Clamp(value, 0, 1);
            fmodParameter.valueParameter = value; 
        }
        
        if (PauseManager.isPaused)
        {
            fmodParameter.valueParameter = 0; 
        }
        
    }
}

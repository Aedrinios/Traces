using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySpeedParticule : MonoBehaviour
{
    public FPS_Controller fps;
    public GameObject speedParticule;

    public float speedTrigger = 10f;
    //public bool withRotation = true;     

    void Update()
    {
        if(fps.velocity.magnitude >= speedTrigger)
        {
            speedParticule.SetActive(true);
            //if (withRotation) speedParticule.transform.rotation = Quaternion.Euler( fps.velocity); 
        }
        else
        {
            speedParticule.SetActive(false);
        }
    }
}

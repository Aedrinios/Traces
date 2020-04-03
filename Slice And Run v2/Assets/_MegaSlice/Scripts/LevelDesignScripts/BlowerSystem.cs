using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerSystem : MonoBehaviour
{
    public GameObject zone;
    public float powerMax = 120f;
    public float ratio = 0.3f;

    //public AnimationCurve curve;  

    FPS_Controller fps;

    //float size;
    float power;
    bool isOn = true; 

    private void Start()
    {
        fps = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
        isOn = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && isOn)
        {
            EquilibreForce(); 
            fps.PushPlayer(Vector3.up * power * Time.deltaTime);
        }
    }

    void EquilibreForce()
    {
        if (fps.velocity.y < 0)
        {
            power = powerMax;
        }
        else
        {
            power = powerMax * ratio; 
        }       
    }

    void ResetOn()
    {
        isOn = true;
    }
}

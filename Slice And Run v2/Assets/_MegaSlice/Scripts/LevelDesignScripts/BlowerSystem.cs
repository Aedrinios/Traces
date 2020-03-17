using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerSystem : MonoBehaviour
{
    public float power = 20;
    
    public AnimationCurve smoothDistance;
    public float distanceMax = 10; 

    float originalPower; 
    FPS_Controller fps;

    private void Start()
    {
        fps = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
        originalPower = power; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerSmooth(); 
            fps.PushPlayer(Vector3.up * power * Time.deltaTime);
        }
    }

    void PowerSmooth()
    {
        float distance =  transform.position.y - FPS_Controller.playerPos.y;
        distance = Mathf.Abs(distance);

        float ratioDistance = distance / distanceMax;
        ratioDistance = Mathf.Clamp(ratioDistance, 0f, 1f); 


        power = originalPower * smoothDistance.Evaluate(ratioDistance); 


        Debug.Log(power); 
    }
}

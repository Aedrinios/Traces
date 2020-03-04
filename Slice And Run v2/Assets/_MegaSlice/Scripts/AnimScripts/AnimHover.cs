using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHover : MonoBehaviour
{


    public float speed = 8;
    public float amount = 3; 
    float ping = 0; 

    private void Update()
    {
        ping = Mathf.PingPong((Time.time/10) * speed , 1);

        Vector3 modifPosition = new Vector3(0, (ping -0.5f) * amount, 0); 

        transform.position += modifPosition * Time.deltaTime;
    }

}

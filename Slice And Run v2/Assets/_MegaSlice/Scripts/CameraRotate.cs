using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRotate : MonoBehaviour
{
    public float speedRotation = 250;
    public float delay = 1f; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(Tilted()); 
        }
    }

    IEnumerator Tilted()
    {
        bool playing = true;
        int reverse = 1;
        float chrono = 0; 

        while (playing)
        {
            chrono += Time.deltaTime; 
            transform.Rotate(Vector3.forward * speedRotation * reverse * Time.deltaTime);

            if (chrono >= delay)
            {
                playing = false; 
            }
            if (chrono >= delay / 2)
            {
                reverse = -1; 
            }         
            
            yield return null;
        }
        
    }



}

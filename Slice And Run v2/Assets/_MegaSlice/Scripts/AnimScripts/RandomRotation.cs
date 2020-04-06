using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public Vector3 axe;
    public float range = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        float random_Range = Random.Range(0f, range); 
        transform.Rotate(axe * random_Range); 
    }


}

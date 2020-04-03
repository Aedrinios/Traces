using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    public GameObject destroyObject;
    float delay = 0.05f; 
    bool canDestroy = false; 

    private void Start()
    {
        Invoke("TriggerDestroy", delay); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && canDestroy)
        {
            if (other.gameObject.tag != "Sliceable")
            {
                Destroy(destroyObject);
            }
        }
    }

    void TriggerDestroy()
    {
        canDestroy = true;
    }
}

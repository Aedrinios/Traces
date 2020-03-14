using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    public GameObject destroyObject;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag != "Sliceable")
            {
                Destroy(destroyObject);
            }
        }
    }
}

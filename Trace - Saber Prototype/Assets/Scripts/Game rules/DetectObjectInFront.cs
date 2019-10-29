using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjectInFront : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            transform.parent.GetComponent<PlayerAttack>().objectInFront = other.gameObject.GetComponent<Sliceable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            transform.parent.GetComponent<PlayerAttack>().objectInFront = null;
        }
    }

}

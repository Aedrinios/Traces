using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjectInFront : MonoBehaviour
{
    public List<GameObject> listNearSliceable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            listNearSliceable.Add(other.gameObject);
         //   transform.parent.GetComponent<PlayerAttack>().objectInFront = other.gameObject.GetComponent<Sliceable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            listNearSliceable.Remove(other.gameObject);
            //   transform.parent.GetComponent<PlayerAttack>().objectInFront = null;
        }
    }

}

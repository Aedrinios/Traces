using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneToSlice : MonoBehaviour
{
    private void Start()
    {
        ChangeSliceableScript(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeSliceableScript(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeSliceableScript(false);
        }
    }

    void ChangeSliceableScript(bool isOn)
    {
        GameObject parent = transform.parent.gameObject;
        SliceableObject sliceScript = parent.GetComponent<SliceableObject>();
        if (sliceScript != null) sliceScript.canSlice = isOn; 
    }


}

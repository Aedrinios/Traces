using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Sliceable objectInFront;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Slicing()
    {
        Debug.Log("KILLER QUEEN");
        if(objectInFront != null)
        {
            Debug.Log("DAISAN NO BAKUDAN");
            float directionComparison = Mathf.Abs(Vector3.Dot(GetComponent<MouseDirection>().direction, objectInFront.WantedDirection));
            if (directionComparison > 0.9f)
            {
                Debug.Log("BITE ZA DUSTO");
                objectInFront.OnDestroy();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [SerializeField] private Vector3 wantedDirection;
    public Vector3 WantedDirection
    {
        get
        {
            /*Vector3 getWanted = (transform.position - wantedDirection).normalized;
            getWanted = new Vector3(getWanted.x, getWanted.y, 0);*/
            return wantedDirection;
        }
        private set
        {
            wantedDirection = value;
        }
    }
    public void Sliced()
    {

    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

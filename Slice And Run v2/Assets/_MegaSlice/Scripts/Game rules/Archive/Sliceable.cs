    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public Vector3 wantedDirection;
    public Vector3 WantedDirection
    {
        get
        {
            Vector3 getWanted = transform.position - wantedDirection;
            getWanted = new Vector3(getWanted.x, getWanted.y, 0);
            return getWanted.normalized;
        }
        private set
        {
            wantedDirection = value;
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

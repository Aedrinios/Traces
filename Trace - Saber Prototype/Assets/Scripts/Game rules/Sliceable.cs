using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public Vector3 wantedDirection;

    public void Sliced()
    {

    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversePlatformPositionWP : MonoBehaviour
{
    private WaypointsMovement wpMovement;

    private void Start()
    {
        wpMovement = GetComponentInParent<WaypointsMovement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wpMovement.InvertTargetPosition();
        }
    }
}


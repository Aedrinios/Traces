using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchBehaviour : MonoBehaviour
{
    [SerializeField] private DoorOpeningScript doorScript;
    private Material currentMaterial;
    [SerializeField] private Material deactivateMaterial;
    private bool isActive;

    private void Start()
    {
        isActive = false;
        doorScript = transform.parent.parent.Find("MovableDoor").GetComponent<DoorOpeningScript>();
            GetComponentInParent<DoorOpeningScript>();
        currentMaterial = GetComponent<Material>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !isActive)
        {
            doorScript.ActivateSwitch();
            LockSwitch();
        }
    }

    private void LockSwitch()
    {
        isActive = true;
        currentMaterial = deactivateMaterial;
    }
}

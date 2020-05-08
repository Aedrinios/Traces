using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchBehaviour : MonoBehaviour
{
    [SerializeField] private DoorOpeningScript doorScript;
    private Renderer myRenderer;
    [SerializeField] private Material deactivateMaterial;
    private bool isActive;

    public float myAngle;
    public float angleTolerance = 25; 

    private void Start()
    {
        isActive = false;
        doorScript = transform.parent.parent.Find("MovableDoor").GetComponent<DoorOpeningScript>();
            GetComponentInParent<DoorOpeningScript>();
        myRenderer = GetComponent<Renderer>();

        myAngle = transform.localEulerAngles.z + 90; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !isActive)
        {
            float projectileAngle = other.gameObject.GetComponentInParent<ProjectileBehaviour>().projectileAngle; 
            float difAngle = Mathf.Abs(myAngle - projectileAngle);
            if (difAngle <= angleTolerance || difAngle >= 180 - angleTolerance)
            {
                doorScript.ActivateSwitch();
                LockSwitch();
            }
            else
            {
                Debug.Log("loupé"); 
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Projectile") && !isActive)
        {
            float projectileAngle = other.gameObject.GetComponentInParent<ProjectileBehaviour>().projectileAngle;
            float difAngle = Mathf.Abs(myAngle - projectileAngle);
            if (difAngle <= angleTolerance || difAngle >= 180 - angleTolerance)
            {
                doorScript.ActivateSwitch();
                LockSwitch();
            }
            else
            {
                Debug.Log("loupé");
            }
        }
    }

    private void LockSwitch()
    {
        isActive = true;
        myRenderer.sharedMaterial = deactivateMaterial;
    }
}

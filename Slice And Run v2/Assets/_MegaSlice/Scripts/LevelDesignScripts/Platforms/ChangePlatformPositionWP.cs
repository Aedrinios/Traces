using UnityEngine;

public class ChangePlatformPositionWP : MonoBehaviour
{
    //Voir dans WaypointsMovement à quoi sert cet enum :
    public WaypointsMovement.Position targetPosition;
    private WaypointsMovement wpMovement;

    private void Start()
    {
        wpMovement = GetComponentInParent<WaypointsMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wpMovement.SetTargetPosition(targetPosition);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaypointsMovement : MonoBehaviour
{
    [SerializeField] private Transform waypointsContainer;
    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private float speed;

    private List<Transform> waypoints = new List<Transform>();
    private bool moving;
    private int targetWaypointId;
    private int direction = -1;

    //Cet enum est essentiellement utilisé par soucis de clareté (voir comment il est utilisé dans SetTargetPosition et
    //InvertTargetPosition plus bas. 
    //Cela permet d'assigner la position cible dans l'inspecteur à partir d'un dropdown nommé clairement plutôt que par
    //un chiffre beaucoup moins clair.
    public enum Position
    {
        Start,
        End
    }
    private Position targetPosition = Position.Start;

    private void Start()
    {
        GetWaypoints();
        targetWaypointId = 0;
        moving = true;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            Vector3 nextPos = waypoints[targetWaypointId].position;
            Vector3 targetPos = Vector3.MoveTowards(platformRb.position, nextPos, speed * Time.deltaTime);
            if (targetPos == nextPos)
            {
                StopOrSetNextPosition();
            }

            platformRb.MovePosition(targetPos);
        }
    }

    private void StopOrSetNextPosition()
    {
        UpdateWaypointId(targetWaypointId + direction);
    }

    private void GetWaypoints()
    {
        waypoints.Clear();
        foreach (Transform child in waypointsContainer)
        {
            waypoints.Add(child);
        }
    }

    private void UpdateWaypointId(int newId)
    {
        targetWaypointId = newId;
        if (targetWaypointId >= waypoints.Count || targetWaypointId < 0)
        {
            moving = false;
            targetWaypointId = Mathf.Clamp(targetWaypointId, 0, waypoints.Count - 1);
        }
        else
        {
            moving = true;
        }
    }

    private void InvertMovement()
    {
        direction = -direction;
        UpdateWaypointId(targetWaypointId + direction);
    }

    #region Public Control
    //Les deux méthodes suivantes permettent de contrôler le mouvement de la plateforme depuis les triggers d'appel et
    //d'inversion de sens.
    public void SetTargetPosition(Position targetPosition)
    {
        if (this.targetPosition != targetPosition)
        {
            this.targetPosition = targetPosition;
            InvertMovement();
        }
    }

    public void InvertTargetPosition()
    {
        switch (targetPosition)
        {
            case Position.Start:
                targetPosition = Position.End;
                break;
            case Position.End:
                targetPosition = Position.Start;
                break;
            default:
                break;
        }

        InvertMovement();
    }
    #endregion

    #region Editor Display
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //On rend visible les waypoints pour rendre leur manipulation plus aisée et la trajectoire plus claire :
        if (waypointsContainer == null) return;

        Color alphaMult = new Color(1, 1, 1, 0.5f);
        Gizmos.color = Color.white * alphaMult;
        Vector3? previousPos = null;
        foreach (Transform child in waypointsContainer)
        {
            Gizmos.DrawWireSphere(child.position, 0.5f);
            if (previousPos != null)
            {
                Gizmos.DrawLine(child.position, previousPos ?? default);
            }
            previousPos = child.position;
        }
    }
#endif
    #endregion
}

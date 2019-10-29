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
            Vector3 getWanted = (transform.position - wantedDirection).normalized;
            getWanted = new Vector3(getWanted.x, getWanted.y, 0);
            return getWanted;
        }
        private set
        {
            wantedDirection = value;
        }
    }

    public void Update()
    {
        Debug.DrawLine(transform.position, (transform.position - wantedDirection).normalized, Color.red);
        Debug.DrawLine(transform.position, (transform.position - wantedDirection), Color.green);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

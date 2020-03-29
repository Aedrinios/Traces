using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWaypoints : MonoBehaviour
{
    public Transform moveBody;
    public Transform waypoints;

    public float speed = 8;
    int way = 0; 

    List<Transform> allWaypoints = new List<Transform>(); 

    private void Start()
    {
        int childLenght = waypoints.childCount; 
        for (int i = 0; i < childLenght; i++)
        {            
            allWaypoints.Add(waypoints.GetChild(i)); 
        }
        way = 1;
        moveBody.transform.position = allWaypoints[0].position; 
    }

    private void FixedUpdate()
    {
        ModifWay();
        float absSpeed = Mathf.Abs(speed); 
        moveBody.position = Vector3.MoveTowards(moveBody.position, allWaypoints[way].position, absSpeed * Time.deltaTime);
    }

    private void Update()
    {
        checkIfExist();
    }

    void ModifWay()
    {
        if (Vector3.Distance(moveBody.position, allWaypoints[way].position) <= 0.1f)
        {
            if (speed >= 0)
            {
                way++;
                if (way >= allWaypoints.Count)
                {
                    way = 0;
                }
            }
            else if (speed < 0)
            {
                way--;
                if (way < 0)
                {
                    way = allWaypoints.Count -1;
                }
            }

        }
    }

    void checkIfExist()
    {
        if (moveBody == null)
        {
            Debug.Log("MoveObjet deleted !!!");
            Destroy(this); 
        }
    }
}

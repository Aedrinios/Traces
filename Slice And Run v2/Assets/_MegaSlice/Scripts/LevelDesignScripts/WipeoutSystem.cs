using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeoutSystem : MonoBehaviour
{
    public Vector3 moveDir = Vector3.forward; 
    public float speed = 10; 
    public bool trigger = false;

    Vector3 endPosition;

    private void Start()
    {
        endPosition = transform.position + transform.TransformDirection(moveDir);
    }

    private void FixedUpdate()
    {
        if (trigger)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime); 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;        

        Vector3 sizeObject = GetComponent<MeshFilter>().sharedMesh.bounds.size;
        Vector3 border = new Vector3(moveDir.normalized.x / 2 * sizeObject.x, moveDir.normalized.y / 2 * sizeObject.y, moveDir.normalized.z / 2 * sizeObject.z);
        border = transform.TransformDirection(border);

        Vector3 gizmosEnd = transform.position + transform.TransformDirection(moveDir) + border;

        Gizmos.DrawLine(transform.position, gizmosEnd); 
        Gizmos.DrawWireSphere(gizmosEnd, 0.2f);       
    }


}

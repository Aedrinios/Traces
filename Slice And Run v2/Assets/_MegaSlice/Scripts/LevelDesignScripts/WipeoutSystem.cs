using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    private void Update()
    {
        if (trigger)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime); 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 sizeObject = Vector3.one;
        if (GetComponent<MeshFilter>()) sizeObject = GetComponent<MeshFilter>().sharedMesh.bounds.size;
        Vector3 border = new Vector3(moveDir.normalized.x / 2 * sizeObject.x, moveDir.normalized.y / 2 * sizeObject.y, moveDir.normalized.z / 2 * sizeObject.z);
        border = new Vector3(border.x * transform.localScale.x, border.y * transform.localScale.y, border.z * transform.localScale.z); 
        border = transform.TransformDirection(border);

        Vector3 gizmosEnd = transform.position + transform.TransformDirection(moveDir) + border;

        Gizmos.DrawLine(transform.position, gizmosEnd); 
        Gizmos.DrawWireSphere(gizmosEnd, 0.2f);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger = true;

            SliceableObject[] sliceScript = GetComponentsInChildren<SliceableObject>();
            for (int i = 0; i < sliceScript.Length; i++)
            {
                sliceScript[i].enabled = true; 
            }

            
        }
    }
}

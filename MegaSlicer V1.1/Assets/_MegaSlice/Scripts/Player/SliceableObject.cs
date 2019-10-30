using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class SliceableObject : MonoBehaviour
{
    GameObject player; 

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Sliceable");        
        GetComponent<MeshCollider>().convex = true;

        player = GameObject.FindWithTag("Player");
        Vector3 expulsion = transform.position - player.transform.position;
        float push = player.GetComponent<PlayerAttack>().forcePush;
        GetComponent<Rigidbody>().AddForce(expulsion * push);
 
    }
}

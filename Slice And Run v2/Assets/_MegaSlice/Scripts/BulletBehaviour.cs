using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float forcePush;

    private Rigidbody rb;
    [HideInInspector] public Transform player;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * forcePush * 100); 
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}

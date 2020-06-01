using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float forcePushCanon = 30;
    [SerializeField] private float forcePushPlayer = 30;
    private Rigidbody rb;    
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * forcePushCanon * 100); 
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            FPS_Controller fpsPlayer = collision.gameObject.GetComponent<FPS_Controller>();
            fpsPlayer.PushPlayer(direction.normalized * forcePushPlayer); 
        }
    }
}

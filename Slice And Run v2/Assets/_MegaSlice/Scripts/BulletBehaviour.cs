using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float forcePush;
    private Rigidbody rb;
    [HideInInspector] public Transform player;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(bulletSpeed * direction);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("bim bam boum");
            rb.velocity = Vector3.zero;
            rb.AddForce(bulletSpeed * -transform.forward);
            Vector3 direction = (player.position - transform.position).normalized;
            player.GetComponent<FPS_Controller>().PushPlayer(direction * forcePush);
        }
    }
}

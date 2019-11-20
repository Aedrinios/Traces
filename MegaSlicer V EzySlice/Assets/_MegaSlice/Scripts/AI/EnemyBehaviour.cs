using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.CompareTag("Player"))
        {
            player.GetComponent<GameOver>().Die(); 
        }
    }
}

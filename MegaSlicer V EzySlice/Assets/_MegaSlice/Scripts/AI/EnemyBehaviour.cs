using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    [SerializeField] private float agentSpeed;
    [SerializeField] private float detectionDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        float playerDistance = Vector3.Distance(target.transform.position, transform.position);
        if(playerDistance < detectionDistance)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;

            agent.speed = agentSpeed;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if(playerDistance <= 1.15f && !BasicTools.isInvicible)
        {
            target.GetComponent<GameOver>().Die();
        }

    }
}

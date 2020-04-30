using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprintwall_Controller : MonoBehaviour
{
    public Animator sp_Wall;
    public BoxCollider boite;

    // Start is called before the first frame update
    void Start()
    {
        sp_Wall = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sp_Wall.Play("SprintWall");
            Destroy(boite);
        }
    }
}

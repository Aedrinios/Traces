using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeoutController : MonoBehaviour
{
    public Animator wipe;
    private SliceableObject sliceableActivation;

    // Start is called before the first frame update
    void Start()
    {
        sliceableActivation = GetComponent<SliceableObject>();
        wipe = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wipe.Play("Take 001");
            sliceableActivation.enabled = true;
        }
    }
}

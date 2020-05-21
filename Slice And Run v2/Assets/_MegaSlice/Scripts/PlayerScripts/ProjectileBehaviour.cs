using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    [SerializeField] private float speed; 

    [HideInInspector] public float projectileAngle; 

    private void Start()
    {
        projectileAngle = transform.localEulerAngles.z;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyFadeout(); 
        }
    }

    public void LoseLife(float lose)
    {
        lifeTime -= lose; 
    }

    public void DestroyFadeout()
    {
        GetComponent<Animator>().enabled = true;
        Invoke("DestroyObject", 0.05f); 
    }
    void DestroyObject()
    {
        Destroy(gameObject); 
    }

}


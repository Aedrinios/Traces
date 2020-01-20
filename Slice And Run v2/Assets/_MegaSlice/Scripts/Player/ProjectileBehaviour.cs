using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeLose = 0.02F;
    [SerializeField] [Range(0, 100)] private float speed;
    public Transform cutPlane;
    public Material crossMaterial;
    public GameObject hitParticle;

    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/InGame/Actions/Weapon/Strike Woosh");
    }

    private void Update()
    {

        transform.position = transform.position + transform.forward * Time.deltaTime * speed;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Slice(GameObject hit)
    {
        if (hit == null)
        {
            return;
        }
        hit.GetComponent<SliceableObject>().Slice(cutPlane);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/InGame/Actions/Environmental Objects/Hit Object");
            Instantiate(hitParticle, transform.position, transform.rotation);
            Slice(other.gameObject);
            lifeTime -= lifeLose;
        }
        else
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/InGame/Actions/Environmental Objects/ImpactWall");
            lifeTime -= lifeLose * 2.5f;
        }
    }
}


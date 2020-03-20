using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerSystem : MonoBehaviour
{
    public GameObject zone;
    public float powerMax = 20;

    public AnimationCurve curve;  

    FPS_Controller fps;

    float size;
    float power; 

    private void Start()
    {
        fps = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
        //originalPower = power; 

        Vector3 colMesh = zone.GetComponent<MeshFilter>().mesh.bounds.size;
        Vector3 scale = zone.transform.localScale; 
        size = colMesh.y * scale.y;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerSmooth(); 
            //EquilibreForce(); 

            fps.PushPlayer(Vector3.up * power * Time.deltaTime);
        }
    }

    // l'idée serait de donner une force au joueur en fonction de sa vitesse de chute pour le stabiliser
    void EquilibreForce()
    {
        power = fps.gravity; 
    }


    // j'arrive pas à faire marcher la curve
    void PowerSmooth()
    {
        float distance =  transform.position.y - FPS_Controller.playerPos.y;
        distance = Mathf.Abs(distance);

        float ratioDistance = distance / size;
        ratioDistance = 1- ratioDistance;  
        ratioDistance = Mathf.Clamp(ratioDistance, 0f, 1f);
        
        power = powerMax * curve.Evaluate(ratioDistance); 
    }
}

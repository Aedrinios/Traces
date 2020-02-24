using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPrefabs : MonoBehaviour
{
    public GameObject prefabsToSpawn;
    public float delay = 1; 
    float chrono = 0; 

    void Update()
    {
        chrono += Time.deltaTime; 

        if (chrono >= delay)
        {
            Instantiate(prefabsToSpawn, transform.position, transform.rotation); 
            chrono = 0; 
        }
    }
}

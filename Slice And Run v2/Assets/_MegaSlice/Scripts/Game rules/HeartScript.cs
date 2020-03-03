using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartScript : MonoBehaviour
{
    public GameObject endGamePrefab;

    SliceableObject sliceScript; 
    private float originalTimeScale;
    private float timePast;
    private bool slowed;

    private void Start()
    {
        sliceScript = GetComponent<SliceableObject>(); 
        originalTimeScale = Time.timeScale;
        timePast = 0f;
        slowed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && sliceScript.canSlice)
        {
            EndGame();            
        }
    }

    void EndGame()
    {
        GameObject go = Instantiate(endGamePrefab, transform.position, transform.rotation);
        
        go.GetComponent<EndGameManager>().slowed = true;
    }
}

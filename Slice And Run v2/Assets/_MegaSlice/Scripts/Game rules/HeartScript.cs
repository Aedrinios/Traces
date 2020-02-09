using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeartScript : MonoBehaviour
{
    public GameObject endGamePrefab;

    private float originalTimeScale;
    private float timePast;
    private bool slowed;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        timePast = 0f;
        slowed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            GameObject go = Instantiate(endGamePrefab, transform.position, transform.rotation);
            go.GetComponent<EndGameManager>().slowed = true;
        }
    }
}

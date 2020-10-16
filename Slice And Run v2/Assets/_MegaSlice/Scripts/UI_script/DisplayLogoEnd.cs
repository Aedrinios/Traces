using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLogoEnd : MonoBehaviour
{
    public float delay = 1.5f;
    public float timeBlockPlayer = 0.5f;
    public float timeRestartPlayer = 4.5f;

    public static bool happened = false;
    FPS_Controller fspScript; 
    Animator myAnimator; 

    // Start is called before the first frame update
    void Start()
    {
        fspScript = FindObjectOfType<FPS_Controller>(); 
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isLevelEnding && !happened)
        {
            Invoke("StartAnim", delay); 
            happened = true; 
        }
    }

    public void StartAnim()
    {
        myAnimator.enabled = true;
        Invoke("BlockPlayer", timeBlockPlayer);
        Invoke("RestartPlayer", timeRestartPlayer);
    }

    public void BlockPlayer()
    {
        fspScript.blockPosition = true;
    }

    public void RestartPlayer()
    {
        fspScript.blockPosition = false;
    }
}

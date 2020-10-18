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


    public void StartAnim()
    {
        myAnimator.enabled = true;
        Invoke("BlockPlayer", timeBlockPlayer);
        Invoke("RestartPlayer", timeRestartPlayer);
    }

    void ShowThanksScreen()
    {
        Invoke("StartAnim", delay);
        happened = true;
    }

    public void OnEnable()
    {
        LevelManager.onLevelComplete += ShowThanksScreen;
    }

    public void OnDisable()
    {
        LevelManager.onLevelComplete -= ShowThanksScreen;
    }

    public void BlockPlayer()
    {
        fspScript.blockPosition = true;
    }

    public void RestartPlayer()
    {
        fspScript.blockPosition = false;
    }

    public void BackToMenu()
    {
        GameManager.Instance.LaunchLevel(0);
    }
}

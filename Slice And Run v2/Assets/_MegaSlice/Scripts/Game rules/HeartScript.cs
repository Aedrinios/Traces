using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeartScript : MonoBehaviour
{
    [Header("Time control")]
    [SerializeField] private float slowPower;
    [SerializeField] private float smooth;
    [SerializeField] private float slowDuration;

    private float originalTimeScale;
    private float timePast;
    private bool slowed;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        timePast = 0f;
        slowed = false;
    }

    private void Update()
    {
        float showTimeScale = Time.timeScale;
        Debug.Log(showTimeScale);
        if (slowed)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowPower, Time.unscaledDeltaTime * smooth);
            timePast += Time.unscaledDeltaTime;
            if (timePast > slowDuration)
            {
                slowed = false;
                timePast = 0f;
                Time.timeScale = originalTimeScale;

                if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                    GameManager.Instance.LaunchLevel(SceneManager.GetActiveScene().buildIndex + 1);
                else
                    GameManager.Instance.LaunchLevel(0);
            }
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, originalTimeScale, Time.unscaledDeltaTime * smooth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            slowed = true;
        }
    }
}

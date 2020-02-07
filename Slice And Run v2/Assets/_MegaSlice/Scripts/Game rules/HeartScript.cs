using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeartScript : MonoBehaviour
{
    [Header("Time control")]
    [SerializeField] private float slowPower;
  //  [SerializeField] private float smooth;
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
        if (slowed)
        {
            Time.timeScale = slowPower;
            timePast += Time.unscaledDeltaTime;
            LifeTimerManager.playing = false;
            if (timePast > slowDuration)
            {
                slowed = false;
                timePast = 0f;
                Time.timeScale = originalTimeScale;
                LevelManager.onLevelComplete?.Invoke();
            }
        }
        else
        {
            //  Time.timeScale = Mathf.Lerp(Time.timeScale, originalTimeScale, Time.unscaledDeltaTime * smooth);
            Time.timeScale = originalTimeScale;

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

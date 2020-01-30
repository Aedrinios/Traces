using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    [Header("Time control")]
    [SerializeField] private float timeSlowDown;
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
        if (slowed)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, timeSlowDown, Time.unscaledDeltaTime * smooth);
            timePast += Time.unscaledDeltaTime;
            if (timePast > slowDuration)
            {
                slowed = false;
                timePast = 0f;
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

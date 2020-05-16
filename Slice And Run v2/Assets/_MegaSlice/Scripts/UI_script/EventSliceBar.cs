using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSliceBar : MonoBehaviour
{
    ChronoSystem chronoSystem;

    public GameObject barUnsliced;
    public GameObject barSliced;
    public float delayStopAnim = 1;

    [Header("Animation bar")]

    public float amplitudeShake;
    public float duration;

    bool isHappen = false; 

    private void Start()
    {
        chronoSystem = GameObject.FindObjectOfType<ChronoSystem>();
        barUnsliced.SetActive(true);
        barSliced.SetActive(false);
        isHappen = false;;
    }

    private void Update()
    {
        if (ChronoSystem.timerStc <= 0 && ChronoSystem.playing && !isHappen)
        {
            isHappen = true;
            StartCoroutine(ShakeUI());
            Invoke("SliceTimeBar", delayStopAnim);
            //Invoke("StopSliceAnim", delayStopAnim); 
        }
    }

    public void SliceTimeBar()
    {
        barUnsliced.SetActive(false);
        barSliced.SetActive(true);
    }

    void StopSliceAnim()
    {
        barSliced.GetComponent<Animator>().enabled = false;
    }

    IEnumerator ShakeUI()
    {
        Debug.Log("SHAAAAKE");
        Vector3 originalPosition = barUnsliced.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * amplitudeShake;
            float y = Random.Range(-1f, 1f) * amplitudeShake;

            barUnsliced.transform.localPosition = new Vector3(x, y, originalPosition.z);

            yield return null;
        }

    }
}

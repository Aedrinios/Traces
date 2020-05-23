using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class EventSliceBar : MonoBehaviour
{
    ChronoSystem chronoSystem;

    public GameObject barUnsliced;
    public GameObject barSliced;
    public float delayStopAnim = 1;

    [Header("Animation bar")]
    public float amplitudeShake;
    public float duration;

    public UnityEvent startShake;
    public UnityEvent endShake;

    bool isHappen = false; 

    private void Start()
    {
        chronoSystem = GameObject.FindObjectOfType<ChronoSystem>();
        barUnsliced.SetActive(true);
        barSliced.SetActive(false);
        isHappen = false;        
    }

    private void Update()
    {
        if (ChronoSystem.timerStc <= delayStopAnim && ChronoSystem.playing && !isHappen)
        {
            isHappen = true;
            startShake.Invoke();
            StartCoroutine(ShakeUI());
            Invoke("SliceTimeBar", delayStopAnim);            
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
        Vector3 originalPosition = barUnsliced.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime; 
            float x = Random.Range(-1f, 1f) * amplitudeShake;
            float y = Random.Range(-1f, 1f) * amplitudeShake;

            barUnsliced.transform.localPosition = new Vector3(x, y, originalPosition.z);

            yield return null;
        }
        endShake.Invoke();

    }
}

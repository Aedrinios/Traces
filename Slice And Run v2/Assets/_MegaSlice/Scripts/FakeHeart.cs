using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeHeart : MonoBehaviour
{
    public GameObject doorFinish;
    public GameObject particleEndPrefab;
    public GameObject soundSliceEnd;
    public float scaleDistance = 1;
    private Transform parent;

    SliceableObject sliceScript;
    private float originalTimeScale;
    private float timePast;
    private bool slowed;

    private void Start()
    {
        parent = transform.parent;
        sliceScript = GetComponent<SliceableObject>();
        originalTimeScale = Time.timeScale;
        timePast = 0f;
        slowed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && sliceScript.canSlice)
        {
            EndGame(other.gameObject.transform);
        }
    }

    void EndGame(Transform projectile)
    {
        GameObject newEffect = Instantiate(particleEndPrefab, parent.position, projectile.rotation) as GameObject;
        float distance = Vector3.Distance(transform.position, FPS_Controller.playerPos);


        Transform[] childs = newEffect.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].localScale *= Mathf.Sqrt((distance + 1)) * scaleDistance;
        }

        doorFinish.GetComponent<FinishDoorManager>().trigger = true; 

        GameObject newSound = Instantiate(soundSliceEnd, transform.position, transform.rotation);

    }
}

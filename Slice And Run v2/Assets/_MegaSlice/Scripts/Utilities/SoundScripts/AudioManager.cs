using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sound;
    [Range(0,1)] public float volume = 1;

    public void PlaySound()
    {
        GameObject newSound = new GameObject("newSound");
        newSound.AddComponent<AudioSource>();
        newSound.AddComponent<DestroyAfter>();

        AudioSource source = newSound.GetComponent<AudioSource>();
        int i = Random.Range(0, sound.Length);
        source.PlayOneShot(sound[i]);
    }
}

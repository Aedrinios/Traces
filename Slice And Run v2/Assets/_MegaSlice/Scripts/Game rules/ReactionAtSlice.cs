using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAtSlice : MonoBehaviour
{
    float volumeDisplay; 

    public GameObject hitSound;

    public void allReactionAtSlice()
    {
        PlayHitSound();
        BonusTimeByVolume(); 
    }

    void BonusTimeByVolume()
    {
        float volumeItem = VolumeOfMesh();
        float bonusTime;
        float numberCuting = GetComponent<SliceableObject>().numberCutting;
        bonusTime = Mathf.Pow(volumeItem, 1.25f); 
        bonusTime = bonusTime * (1 / (numberCuting * 2 + 1));
        bonusTime = Mathf.Clamp(bonusTime, 0, 2.5f);
        bonusTime = bonusTime * LifeTimerManager.multiplierBonusCutStatic;         

        LifeTimerManager.lifeTimer += bonusTime; 
    }

    void PlayHitSound()
    {
        if (hitSound != null) Instantiate(hitSound, transform.position, transform.rotation);
    }
    
    float VolumeOfMesh()
    {
        Mesh myMesh = GetComponent<MeshFilter>().mesh;
        Vector3 mySize = myMesh.bounds.size;
        float volume = mySize.x * mySize.y * mySize.z;
        // rajout du scale 
        Vector3 myScale = transform.localScale;
        volume = volume * myScale.x * myScale.y * myScale.z; 

        return Mathf.Abs(volume); 
    }


}

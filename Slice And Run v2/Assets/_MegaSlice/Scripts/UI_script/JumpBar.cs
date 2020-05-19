using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class JumpBar : MonoBehaviour
{
    public Sprite emptyBar;
    Sprite originalSprite; 
    public int id = 0;
    public float speedFadeOut = 15f; 

    Color originalColor; 
    WallJump wallJumpScript;
    FPS_Controller fps;
    Image img;

    bool increase = false;
    float value = 0;
    bool isOn = false; 

    void Start()
    {
        wallJumpScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WallJump>();
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS_Controller>();
        img = GetComponent<Image>();
        originalSprite = img.sprite;
        originalColor = img.color;
        img.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        isOn = false;
        Invoke("PutOn", 0.5f); 
    }
    
    void Update()
    {
        if (isOn)
        {
            if (fps.onGround) increase = false;
            else increase = true;

            ChangeColor();
            ChangeSprite();
        } 
    }

    void ChangeColor()
    {
        Color colorActual = originalColor; 
        if (increase)
        {
            value = Mathf.MoveTowards(value, 1, speedFadeOut * Time.deltaTime); 
        }
        else
        {
            value = Mathf.MoveTowards(value, 0, speedFadeOut * Time.deltaTime);
        }
        value = Mathf.Clamp(value, 0, 1);
        colorActual.a = value; 
        img.color = colorActual; 
    }

    void ChangeSprite()
    {
        if (wallJumpScript.countJump < id)
        {
            img.sprite = originalSprite;            
        }
        else
        {
            img.sprite = emptyBar;            
        }
    }

    void PutOn()
    {
        isOn = true; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class JumpBar : MonoBehaviour
{
    public Sprite emptyBar;
    Sprite originalSprite; 
    public int id = 0;
    public float alphaEmpty = 0.3f; 

    Color originalColor; 
    WallJump wallJumpScript;
    FPS_Controller fps;
    Image img; 

    // Start is called before the first frame update
    void Start()
    {
        wallJumpScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WallJump>();
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS_Controller>();
        img = GetComponent<Image>();
        originalSprite = img.sprite;
        originalColor = img.color; 
    }

    // Update is called once per frame
    void Update()
    {
        if (fps.onGround)
        {
            img.enabled = false;
        }
        else
        {
            img.enabled = true;
        }
        ChangeSprite(); 
    }

    void ChangeSprite()
    {
        if (wallJumpScript.countJump < id)
        {
            img.sprite = originalSprite;
            img.color = originalColor; 
        }
        else
        {
            img.sprite = emptyBar;
            img.color = originalColor - new Color(0, 0, 0, alphaEmpty); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public delegate void OnLevelUnlocked();
    public OnLevelUnlocked onLevelUnlocked;

    public int id;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite unlockedSpriteHighlight;
    private Button button;
    private Image currentButtonImage;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        currentButtonImage = GetComponent<Image>();
        onLevelUnlocked += UnlockLevel;
    }

    private void OnDisable()
    {
        onLevelUnlocked -= UnlockLevel;
    }

    public void UnlockLevel()
    {
        SpriteState unlockedState = new SpriteState();
        unlockedState.highlightedSprite = unlockedSpriteHighlight;
        button.interactable = true;
        button.spriteState = unlockedState;
        currentButtonImage.sprite = unlockedSprite;

    }
}

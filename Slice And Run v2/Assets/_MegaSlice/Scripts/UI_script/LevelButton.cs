using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public delegate void OnLevelUnlocked();
    public OnLevelUnlocked onLevelUnlocked;

    public delegate void OnLevelLocked();
    public OnLevelLocked onLevelLocked;

    public int id;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite unlockedSpriteHighlight;
    [SerializeField] private Sprite lockedSprite;
    private Button button;
    private Image currentButtonImage;
    private Color aColor;
    private GameObject rankObject;
    private void OnEnable()
    {
        button = GetComponent<Button>();
        currentButtonImage = GetComponent<Image>();
        onLevelUnlocked += UnlockLevel;
        onLevelLocked += LockLevel;
        rankObject = transform.Find("Rank").gameObject;

        if (id > ProgressionManager.numberOfLevel - 1)
        {
            gameObject.SetActive(false); 
        }

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if(playerManager.scoreList[id] > 0.0f)
        {
            rankObject.SetActive(true);
            rankObject.GetComponentInChildren<TextMeshProUGUI>().text = playerManager.rankList[id];
            if (playerManager.rankList[id] == "A")
            {
                aColor = new Color(1, 0.1607843f, 0.3215686f, 1);
                rankObject.GetComponent<Image>().color = aColor;
            }
        }
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

    public void LockLevel()
    {
        button.interactable = false;
        currentButtonImage.sprite = lockedSprite;
        rankObject.SetActive(false);
    }
}

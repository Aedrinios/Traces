using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public delegate void OnLevelUnlocked();
    public OnLevelUnlocked onLevelUnlocked;

    public int id;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite unlockedSpriteHighlight;
    private Button button;
    private Image currentButtonImage;
    public Color aColor;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        currentButtonImage = GetComponent<Image>();
        onLevelUnlocked += UnlockLevel;

        if (id > ProgressionManager.numberOfLevel - 1)
        {
            gameObject.SetActive(false); 
        }

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if(playerManager.scoreList[id] > 0.0f)
        {
            GameObject rankObject = transform.Find("Rank").gameObject;
            rankObject.SetActive(true);
            rankObject.GetComponentInChildren<TextMeshProUGUI>().text = playerManager.rankList[id];
            if (playerManager.rankList[id] == "A")
            {
                Debug.Log("hello score A");
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
}

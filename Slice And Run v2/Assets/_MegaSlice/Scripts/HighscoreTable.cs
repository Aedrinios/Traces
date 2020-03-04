using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HighscoreTable : MonoBehaviour
{
    public float templateHeigth;

    private Transform entryTemplate;
    private Transform entryContainer;
    private PlayerManager playerManager;
    private List<PlayerData> allPlayersData = new List<PlayerData>();

    private int playerIndex;
    private bool playerInLeaderboard = false;

    private void Awake()
    {
        entryContainer = transform.Find("entryContainer");
        entryTemplate = entryContainer.Find("entryTemplate");
        playerManager = FindObjectOfType<PlayerManager>();
        entryTemplate.gameObject.SetActive(false);
    }

    public void SortPlayerList(int levelIndex)
    {
        allPlayersData = SaveSystem.LoadAllPlayersFromLevel(levelIndex);
        for (int i = 0; i < allPlayersData.Count; i++)
        {
            for (int j = i + 1; j < allPlayersData.Count; j++)
            {
                if (allPlayersData[j].scoreList[levelIndex] < allPlayersData[i].scoreList[levelIndex])
                {
                    // Swap
                    PlayerData tmp = allPlayersData[i];
                    allPlayersData[i] = allPlayersData[j];
                    allPlayersData[j] = tmp;
                }
            }
        }
        for (int i = 0; i < allPlayersData.Count; i++)
        {
            if (allPlayersData[i].name == playerManager.name)
            {
                playerIndex = i;
            }
        }

        int numberOfHighscore = allPlayersData.Count < 9 ? allPlayersData.Count : 9;

        int rank = 1;
        for (int i = 0; i < numberOfHighscore; i++)
        {
            InstantiateLeaderboard(rank, i, levelIndex, i);
            rank += 1;  
        }
        if (!playerInLeaderboard)
        {
            InstantiateLeaderboard(playerIndex, playerIndex, levelIndex, 9);
        }
    }

    private void InstantiateLeaderboard(int rank, int indexInList, int levelIndex, int transformIncrementation)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector3(0, -templateHeigth * transformIncrementation, 0);
        entryTransform.gameObject.SetActive(true);

        TextMeshProUGUI rankText = entryTransform.Find("rankText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nameText = entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timeText = entryTransform.Find("timeText").GetComponent<TextMeshProUGUI>();

        rankText.text = rank.ToString();
        nameText.text = allPlayersData[indexInList].name;
        TimeSpan timeSpan = TimeSpan.FromSeconds(allPlayersData[indexInList].scoreList[levelIndex]);

        timeText.text = string.Format("{0:D2}:{1:D2}:{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

        if (playerManager.name == nameText.text)
        {
            Debug.Log("ouille ouille ouille");
            playerInLeaderboard = true;
            rankText.color = Color.red;
            nameText.color = Color.red;
            timeText.color = Color.red;
        }
    }
}

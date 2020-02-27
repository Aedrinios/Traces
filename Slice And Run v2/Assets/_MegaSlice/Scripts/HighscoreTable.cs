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

    private void Awake()
    {
        entryContainer = transform.Find("entryContainer");
        entryTemplate = entryContainer.Find("entryTemplate");

        entryTemplate.gameObject.SetActive(false);
    }

    public void SortPlayerList(int levelIndex)
    {
        List<PlayerData> allPlayersData = SaveSystem.LoadAllPlayers();
        for (int i = 0; i < allPlayersData.Count; i++)
        {
            for (int j = i + 1; j < allPlayersData.Count; j++)
            {
                if (allPlayersData[j].scoreList[levelIndex] > allPlayersData[i].scoreList[levelIndex])
                {
                    // Swap
                    PlayerData tmp = allPlayersData[i];
                    allPlayersData[i] = allPlayersData[j];
                    allPlayersData[j] = tmp;
                }
            }
        }
        int numberOfHighscore = allPlayersData.Count < 10 ? allPlayersData.Count : 10;

        for (int i = 0; i < numberOfHighscore; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector3(0, -templateHeigth * i, 0);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            TextMeshProUGUI rankText = entryTransform.Find("rankText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timeText = entryTransform.Find("timeText").GetComponent<TextMeshProUGUI>();

            rankText.text = rank.ToString();
            nameText.text = allPlayersData[i].name;
            TimeSpan timeSpan = TimeSpan.FromSeconds(allPlayersData[i].scoreList[levelIndex]);

            timeText.text = string.Format("{0:D2}:{1:D2}:{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            if(FindObjectOfType<PlayerManager>().name == nameText.text)
            {
                rankText.color = Color.red;
                nameText.color = Color.red;
                timeText.color = Color.red;
            }
        }
    }
}

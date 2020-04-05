using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";


    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void CreateFile(PlayerManager player)
    {
        PlayerData data;
        if(!File.Exists(SAVE_FOLDER + "/Save_" + player.name + ".txt"))
        {
            float[] initScoreList = new float[player.scoreList.Length];
            player.scoreList = initScoreList;
            data = new PlayerData(player.name, initScoreList);
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(SAVE_FOLDER + "/Save_" + player.name + ".txt", jsonData);
        }
        else
        {
            data = LoadPlayer(player.name);
            player.name = data.name;
            player.scoreList = data.scoreList;
            Debug.LogError("File /Save_" + player.name + ".txt already exist");
        }
    }

    public static void SavePlayer(PlayerManager player)
    {
        /* BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Create);*/
        PlayerData data = new PlayerData(player.name, player.scoreList);
        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(SAVE_FOLDER + "/Save_" + player.name + ".txt", jsonData);

        /* formatter.Serialize(stream, data);
         stream.Close();*/
    }

    public static List<PlayerData> LoadAllPlayersFromLevel(int levelIndex)
    {
        List<PlayerData> allPlayersData = new List<PlayerData>();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        foreach (FileInfo fileInfo in saveFiles)
        {
            if(fileInfo != null)
            {
                string savedData = File.ReadAllText(fileInfo.FullName);
                PlayerData data = JsonUtility.FromJson<PlayerData>(savedData);
                if(data.scoreList.Length > levelIndex)
                {
                    if (data.scoreList[levelIndex] > 0)
                    {
                        allPlayersData.Add(data);
                    }
                }

            }
        }
        return allPlayersData;
    }

    public static PlayerData LoadGame()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        } 
        if(mostRecentFile != null)
        {
            string savedData = File.ReadAllText(mostRecentFile.FullName);
            PlayerData data = JsonUtility.FromJson<PlayerData>(savedData);
            return data;
        }
        else
        {
            Debug.LogError("First time playing");
            return null;
        }
    }

    public static PlayerData LoadPlayer(string playerName)
    {
        if (File.Exists(SAVE_FOLDER + "/Save_" + playerName +".txt"))
        {
            /* BinaryFormatter formatter = new BinaryFormatter();
              FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;*/
            string savedData = File.ReadAllText(SAVE_FOLDER + "/Save_" + playerName + ".txt");

            PlayerData data = JsonUtility.FromJson<PlayerData>(savedData);

            return data;
        }
        else
        {
            Debug.LogError("File not found in " + SAVE_FOLDER);
            return null;
        }
    }
}

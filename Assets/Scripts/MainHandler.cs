using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainHandler : MonoBehaviour
{
    public static MainHandler Instance;

    public string playerName;
    public int playerScore;
    public string playerHighScoreName;
    public int playerHighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadNameAndScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerHighScoreName;
        public int playerHighScore;
    }

    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();

        data.playerHighScoreName = playerHighScoreName;
        data.playerHighScore = playerHighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerHighScoreName = data.playerHighScoreName;
            playerHighScore = data.playerHighScore;
        }
    }
}
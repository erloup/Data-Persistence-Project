using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public struct PlayerScore
    {
        public string name;
        public int score;
    }
    public static MainManager Instance;
    public string name;
    public List<PlayerScore> scores;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public List<PlayerScore> scores;
        public string name;
    }

    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.scores = scores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadInfo() 
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
            scores = data.scores;
            if (scores == null) scores = new List<PlayerScore>();
        }
        else scores = new List<PlayerScore>();
    }

}

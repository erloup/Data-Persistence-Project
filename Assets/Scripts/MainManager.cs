using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public string name;
    public Dictionary<string, int> scores;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public Dictionary<string, int> scores;
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
        }
    }

}

using System.IO;
using UnityEngine;

public class ScoreSaveLoad : Singleton<ScoreSaveLoad>, IData
{
    private const string KEY = "score.json";
    public void Save()
    {
        ScoreManager score = GameObject.FindFirstObjectByType<ScoreManager>();
        if (score == null) return;
        ScoreData data = new ScoreData( score.bestScore);
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + KEY;
        File.WriteAllText(path, json);
    }
    public void Load()
    {
        string path = Application.persistentDataPath + KEY;
        if (!File.Exists(path)) return;
        Debug.Log("File ScoreData" + path);
        string json = File.ReadAllText(path);
        ScoreData data = JsonUtility.FromJson<ScoreData>(json);
        ScoreManager score = GameObject.FindFirstObjectByType<ScoreManager>();
        if (score == null) return;
        score.bestScore = data.bestScore;
    }

    public void Delete()
    {
        ScoreManager score = GameObject.FindFirstObjectByType<ScoreManager>();
        string path = Application.persistentDataPath + KEY;
        if(File.Exists(path))
            File.Delete(path);
        score.Reset();
    }
}

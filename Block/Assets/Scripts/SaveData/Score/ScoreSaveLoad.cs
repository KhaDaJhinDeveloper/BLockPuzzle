using System.IO;
using UnityEngine;

public class ScoreSaveLoad : Singleton<ScoreSaveLoad>, IData
{
    public void Save()
    {
        ScoreManager score = GameObject.FindFirstObjectByType<ScoreManager>();
        if (score == null) return;
        ScoreData data = new ScoreData(score.CurentScore, score.BestScore);
        string json = JsonUtility.ToJson(data);
    }
    public void Load()
    {
        //string json = File.ReadAllText();
        //ScoreData data = JsonUtility.FromJson<ScoreData>(json);
    }

    public void Delete()
    {
        throw new System.NotImplementedException();
    }
}

using System.IO;
using UnityEngine;

public class VolumeSaveAndLoad : Singleton<VolumeSaveAndLoad>,IData
{
    private const string KEY = "sound.json";
    public string path;
    public void Save()
    {
        SoundManager soundManager = SoundManager.Instance;
        if (soundManager == null) return;
        VolumeData data = new VolumeData(soundManager.volumeBG, soundManager.volumeSFX);
        string json = JsonUtility.ToJson(data);
         path = Application.persistentDataPath + KEY;
        File.WriteAllText(path, json);
    }
    public void Load()
    {
        SoundManager soundManager = SoundManager.Instance;
        if (soundManager == null) Debug.Log(null);
        path = Application.persistentDataPath + KEY;
        if (!File.Exists(path))
        {         
            soundManager.volumeBG = 1f;
            soundManager.volumeSFX = 1f;
            return;
        }
        string json = File.ReadAllText(path);
        VolumeData data = JsonUtility.FromJson<VolumeData>(json);
        soundManager.volumeBG = data.volumeBG;
        soundManager.SetVolumeBG(data.volumeBG);
        soundManager.volumeSFX = data.volumeSFX;
        soundManager.SetVolumeSFX(data.volumeSFX);
    }
    public void Delete()
    {
         path = Application.persistentDataPath + KEY;
         if (File.Exists(path))
            File.Delete(path);  
    }
    public bool HasData()
    {
         path = Application.persistentDataPath + KEY;
        return File.Exists(path);
    }
}

using System.IO;
using UnityEngine;

public class VolumeSaveAndLoad : Singleton<VolumeSaveAndLoad>,IData
{
    private const string KEY = "sound.json";
    public void Save()
    {
        SoundManager soundManager = SoundManager.Instance;
        if (soundManager == null) return;
        VolumeData data = new VolumeData(soundManager.volumeBG, soundManager.volumeSFX);
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + KEY;
        File.WriteAllText(path, json);
    }
    public void Load()
    {
        string path = Application.persistentDataPath + KEY;
        if(path == null) return;
        string json = File.ReadAllText(path);
        VolumeData data = JsonUtility.FromJson<VolumeData>(json);
        SoundManager soundManager = SoundManager.Instance;
        if(soundManager == null) return;
        soundManager.volumeBG = data.volumeBG;
        soundManager.SetVolumeBG(data.volumeBG);
        soundManager.volumeSFX = data.volumeSFX;
        soundManager.SetVolumeSFX(data.volumeSFX);
    }
    public void Delete()
    {
        string path = Application.persistentDataPath + KEY;
        if (File.Exists(path))
            File.Delete(path);
    }
    public bool HasData()
    {
        string path = Application.persistentDataPath + KEY;
        return File.Exists(path);
    }
}

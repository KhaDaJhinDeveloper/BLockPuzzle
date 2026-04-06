using System.IO;
using UnityEngine;
public class SaveLoadData : Singleton<SaveLoadData>,IData
{
    public void Save()
    {
        ScoreSaveLoad.Instance.Save();
        VolumeSaveAndLoad.Instance.Save();
    }
    public void Load()
    {
        ScoreSaveLoad.Instance.Load();
        VolumeSaveAndLoad.Instance.Load();
    }
    public void Delete()
    {
        ScoreSaveLoad.Instance.Delete();
        VolumeSaveAndLoad.Instance.Delete();
    }
}

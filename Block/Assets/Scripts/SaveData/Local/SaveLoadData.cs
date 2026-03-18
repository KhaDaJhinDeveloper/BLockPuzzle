using UnityEngine;
using System.IO;
public class SaveLoadData : Singleton<SaveLoadData>,IData
{
    public void Save()
    {
        ScoreSaveLoad.Instance.Save();
    }
    public void Load()
    {
        
    }
    public void Delete()
    {
        
    }
}

using UnityEngine;
using System.Collections;

public class ButtonDeleteAllData: BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();        
        SaveLoadData.Instance.Delete();
    }
}
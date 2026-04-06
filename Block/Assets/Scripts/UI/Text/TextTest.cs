using UnityEngine;

public class TextTest : BaseText
{
    protected override void Update()
    {
        base.Update();
        SetText(VolumeSaveAndLoad.Instance.path);
    }
}

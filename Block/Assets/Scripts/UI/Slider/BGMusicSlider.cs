using UnityEngine;

public class BGMusicSlider : BaseSlider
{
    protected override void LoadSlider(float value)
    {
        base.LoadSlider(value);
        SoundManager.Instance.SetVolumeBG(value);
    }
}

using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BGMusicSlider : BaseSlider
{
    protected override void Start()
    {
        base.Start();
        LoadSlider(SoundManager.Instance.volumeBG);
        this.slider.value = SoundManager.Instance.volumeBG;
    }
    protected override void LoadSlider(float value)
    {
        base.LoadSlider(value);
        SoundManager.Instance.SetVolumeBG(value);
        SoundManager.Instance.volumeBG = value;
    }
}

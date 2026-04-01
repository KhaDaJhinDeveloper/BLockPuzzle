using UnityEngine;

public class SFXMusicSlider : BaseSlider
{
    protected override void Start()
    {
        base.Start();
        if (VolumeSaveAndLoad.Instance.HasData())
        {
            LoadSlider(SoundManager.Instance.volumeSFX);
            this.slider.value = SoundManager.Instance.volumeSFX;
        }
    }
    protected override void LoadSlider(float value)
    {
        base.LoadSlider(value);
        SoundManager.Instance.SetVolumeSFX(value);
        SoundManager.Instance.volumeSFX = value;
    }
}

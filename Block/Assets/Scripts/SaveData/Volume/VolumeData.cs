using UnityEngine;
[System.Serializable]
public class VolumeData 
{
    public float volumeBG;
    public float volumeSFX;
    public VolumeData() { }
    public VolumeData(float volumeBG, float volumeSFX)
    {
        this.volumeBG = volumeBG;
        this.volumeSFX = volumeSFX;
    }
}

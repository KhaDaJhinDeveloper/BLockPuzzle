using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
[Header("Sound")]
    #region BG
    public AudioClip bg_MainMenu;
    public AudioClip bg_playingGame;
    #endregion
    #region SFX
    public AudioClip sfx_BClickButton;
    public AudioClip sfx_PlaceBlock;
    public AudioClip sfx_Score;
    public AudioClip sfx_GameOver;
    public AudioClip sfx_NewScore;
    public AudioClip sfx_BlockDown;
    #endregion
[Header("Pool")]
    #region Initialize
    [SerializeField] private AudioSource bgSource;
    [SerializeField] private AudioSource sfxSource;
    private AudioSource currentBg;
    private const string Key_BG = "bgSource";
    private const string Key_SFX = "sfxSource";
    #endregion
    public float volumeSound;
    private AudioSource[] obj;
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
    private void Start()
    {
        this.obj = GameObject.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        //SetVolume();
    }
    public void Initialize()
    {
        ObjectPooling.Instance.CreatePool(Key_BG, bgSource.gameObject, 3);
        ObjectPooling.Instance.CreatePool(Key_SFX, sfxSource.gameObject, 5);
    }
    #region BG Music
    public void PlayMusicBG(AudioClip clip)
    {
        if(this.currentBg != null)
        {
            this.currentBg.Stop();
            ObjectPooling.Instance.ReturnToPool(Key_BG, this.currentBg.gameObject);
            this.currentBg = null;
        }    
        AudioSource bg = ObjectPooling.Instance.GetPool(Key_BG).GetComponent<AudioSource>();
        bg.clip = clip;
        bg.Play();
        this.currentBg = bg;
    }
    public void StopMusicBG(AudioClip clip)
    {

    }
    #endregion
    #region SFX Music
    public void PlaySFX(AudioClip clip)
    {
        AudioSource sfx = ObjectPooling.Instance.GetPool(Key_SFX).GetComponent<AudioSource>();
        sfx.clip = clip;
        sfx.Play();
        StartCoroutine(ReturnSFXToPool(sfx, clip.length));
    }
    IEnumerator ReturnSFXToPool(AudioSource clip,float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooling.Instance.ReturnToPool(Key_SFX, clip.gameObject);
    }    
    #endregion
    public void SetVolumeBG(float value)
    {
        foreach(AudioSource o in obj)
            o.volume = value;
    }
    public void SetVolumeSFX(float value)
    {
        foreach (AudioSource o in obj)
            o.volume = value;
    }    
}


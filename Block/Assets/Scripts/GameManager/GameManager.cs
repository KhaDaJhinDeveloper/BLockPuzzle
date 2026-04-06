using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public bool newBestScore = false;
    private Board board;
    private ScoreManager ScoreManager;
    private Blocks blocks;
    [SerializeField]private ParticleSystem partical;
    protected override void Awake()
    {
        base.Awake();
        SaveLoadData.Instance.Load();  
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnloadScene;
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.blocks = GameObject.FindFirstObjectByType<Blocks>();
        this.ScoreManager = GameObject.FindFirstObjectByType<ScoreManager>();
        StartCoroutine(Delay());
    }
    private void Reset()
    {
        this.newBestScore = false;
    }
    void OnloadScene(Scene scene, LoadSceneMode mode)
    {
        Reset();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnloadScene;
    }
    public void Play()
    {
        StartCoroutine(PlayGame());
    }
    public void ReturnMainMenu()
    {
        StartCoroutine(MainMenu());
    }
    public void Retry()
    {
        this.partical.Play();
        LoadComponents();
    }    
    public void GameOVer()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_GameOver);
        EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWGAMEOVER);
    }
    private void LoadComponents()
    { 
        this.newBestScore = false;
        this.board.Retry();
        this.blocks.Retry();
        this.ScoreManager.Retry();
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_MainMenu);
    }
    private IEnumerator PlayGame()
    {
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_playingGame);
        this.partical.Play();
        yield return new WaitForSeconds(1f);
        LoadComponents();
        EventManager.Instance.TriggerEvent(EventName.EVENT_HIDEMAINMENU);
        EventManager.Instance.TriggerEvent(EventName.EVENT_HIDEMENUPAUSE);
    }
    private IEnumerator MainMenu()
    {
        Time.timeScale = 1f;
        this.partical.Play();
        yield return new WaitForSeconds(1f);
        EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWMAINMENU);
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_MainMenu);
}
    private void OnApplicationQuit()
    {
        SaveLoadData.Instance.Save();
    }
}

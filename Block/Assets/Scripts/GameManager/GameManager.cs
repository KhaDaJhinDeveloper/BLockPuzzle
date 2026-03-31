using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public bool newBestScore = false;
    private Board board;
    private ScoreManager ScoreManager;
    private Blocks blocks;
    private ParticleSystem partical;
    private void Start()
    {
        SceneManager.sceneLoaded += OnloadScene;
        StartCoroutine(Delay());
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_MainMenu);
        SaveLoadData.Instance.Load();
        this.partical = GameObject.FindFirstObjectByType<ParticleSystem>();
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
        LoadComponents();
    }    
    public void GameOVer()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_GameOver);
        EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWGAMEOVER);
    }
    private void LoadComponents()
    {
        this.partical.Play();
        this.newBestScore = false;
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.blocks = GameObject.FindFirstObjectByType<Blocks>();
        this.ScoreManager = GameObject.FindFirstObjectByType<ScoreManager>();
        if(this.board == null && this.blocks == null && this.ScoreManager == null) return;
        this.board.Retry();
        this.blocks.Retry();
        this.ScoreManager.Retry();
    }
    private IEnumerator Delay()
    {
        yield return null;
    }
    private IEnumerator PlayGame()
    {
        this.partical.Play();
        yield return new WaitForSeconds(partical.main.duration * 0.25f);
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_playingGame);
        EventManager.Instance.TriggerEvent(EventName.EVENT_HIDEMAINMENU);
    }
    private IEnumerator MainMenu()
    {
        Time.timeScale = 1f;
        this.partical.Play();
        yield return new WaitForSeconds(partical.main.duration * 0.25f);
        EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWMAINMENU);
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_MainMenu);
    }
    private void OnApplicationQuit()
    {
        SaveLoadData.Instance.Save();
    }
}

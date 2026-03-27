using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    void Start()
    {
        EventManager.Instance.Subscribe(EventName.EVENT_SHOWMENUPAUSE, GamePauseShow);
        EventManager.Instance.Subscribe(EventName.EVENT_HIDEMENUPAUSE, GamePauseHide);
        GamePauseHide();
    }
    public void GamePauseShow()
    {
        this.menuPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GamePauseHide()
    {
        this.menuPause.SetActive(false);
        Time.timeScale = 1f;
    }
    //private void OnDestroy()
    //{
    //    EventManager.Instance.UnSubscribe(EventName.EVENT_SHOWMENUPAUSE, GamePauseShow);
    //    EventManager.Instance.UnSubscribe(EventName.EVENT_HIDEMENUPAUSE, GamePauseHide);
    //}
}

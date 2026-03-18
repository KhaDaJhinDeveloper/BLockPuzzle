using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver_UI;
    private void Start()
    {
        EventManager.Instance.Subscribe(EventName.EVENT_SHOWGAMEOVER, UIShow);
        EventManager.Instance.Subscribe(EventName.EVENT_HIDEGAMEOVER, UIHide);
        this.gameOver_UI.SetActive(false);
    }
    private void UIShow()
    {
        this.gameOver_UI.SetActive(true);
    }
    private void UIHide()
    {
        this.gameOver_UI.SetActive(false); 
    }
    private void OnDestroy()
    {
        if (EventManager.Instance == null)
        {
            Debug.Log("null");
            return;
        }

        EventManager.Instance.UnSubscribe(EventName.EVENT_SHOWGAMEOVER, UIShow);
        EventManager.Instance.UnSubscribe(EventName.EVENT_HIDEGAMEOVER, UIHide);
    }
}

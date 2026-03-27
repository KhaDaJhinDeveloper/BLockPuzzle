using UnityEngine;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    void Start()
    {
        EventManager.Instance.Subscribe(EventName.EVENT_SHOWMAINMENU, MainMenuShow);
        EventManager.Instance.Subscribe(EventName.EVENT_HIDEMAINMENU, MainMenuHide);
    }
    void MainMenuShow()
    {
        this.mainMenu.SetActive(true);
    }
    void MainMenuHide()
    {
        this.mainMenu.SetActive(false);
    }
    //private void OnDestroy()
    //{
    //    EventManager.Instance.UnSubscribe(EventName.EVENT_SHOWMAINMENU, MainMenuShow);
    //    EventManager.Instance.UnSubscribe(EventName.EVENT_HIDEMAINMENU, MainMenuHide);
    //}
}

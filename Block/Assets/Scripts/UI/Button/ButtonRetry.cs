using UnityEngine;

public class ButtonRetry : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameManager.Instance.Retry();
        EventManager.Instance.TriggerEvent(EventName.EVENT_HIDEGAMEOVER);
    }
}

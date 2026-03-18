using UnityEngine;

public class ButtonPause : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        EventManager.Instance.TriggerEvent(EventName.EVENT_SHOWMENUPAUSE);
    }
}

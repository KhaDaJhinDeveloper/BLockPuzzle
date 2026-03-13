using UnityEngine;

public class ButtonMenuPause : BaseButton
{
    protected override void AddOnClickEvent()
    {
        base.AddOnClickEvent();
        Debug.Log("MenuPause");
    }
}

using UnityEngine;

public class ButtonReturnMainMenu : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameManager.Instance.ReturnMainMenu();
    }
}

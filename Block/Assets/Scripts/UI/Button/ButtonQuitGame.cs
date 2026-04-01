using UnityEngine;

public class ButtonQuitGame : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        Application.Quit();
    }
}

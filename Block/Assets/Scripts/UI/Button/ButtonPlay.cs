using UnityEngine;

public class ButtonPlay : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameManager.Instance.Play();
    }
}

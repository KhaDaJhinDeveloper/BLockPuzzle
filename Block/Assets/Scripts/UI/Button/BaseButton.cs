using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BaseButton : MonoBehaviour
{
    protected Button button;  
    protected virtual void Start()
    {
        LoadComponents();
        AddOnClickEvent();
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(OnClick);
    }
    protected virtual void OnClick()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_BClickButton);
    }
    protected virtual void LoadComponents()
    {
        this.button = GetComponent<Button>();
    }
}

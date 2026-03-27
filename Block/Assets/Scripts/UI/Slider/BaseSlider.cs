using UnityEngine;
using UnityEngine.UI;

public class BaseSlider : MonoBehaviour
{
    protected Slider slider;
    protected virtual void Start()
    {
        LoadComponents();
        AddOnSliderValueChanged();
    }
    protected virtual void LoadComponents()
    {
        this.slider = GetComponent<Slider>();
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void LoadSlider(float value)
    {
        
    }    
    protected virtual void AddOnSliderValueChanged()
    {
        this.slider.onValueChanged.AddListener(LoadSlider);   
    }
}

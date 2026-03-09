using TMPro;
using UnityEngine;

public class BaseText : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    protected virtual void Start()
    {
        this.m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    protected virtual void Update()
    {

    } 
    protected virtual void SetText(string text)
    {
        this.m_TextMeshProUGUI.text = text.ToString();
    }
}

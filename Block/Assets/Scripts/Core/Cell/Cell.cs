using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] Sprite spriteHighlight;
    [SerializeField] Sprite spriteNormal;
    private SpriteRenderer spriteRender;
    void Start()
    {
        this.spriteRender = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    public void Normal()
    {
        SetupCell(true, this.spriteNormal, Color.white);
    }
    public void Hover()
    {
        Color colorHover = new(1.0f, 1.0f, 1.0f, 0.5f);
        SetupCell(true, this.spriteNormal, colorHover);
    } 
    public void HighLight()
    {
        SetupCell(true, this.spriteHighlight, Color.white);
    }
    public void Hide()
    {
        SetupCell(false, this.spriteNormal, Color.white);
    }
    public void SetupCell(bool setactive, Sprite sprite, Color color)
    {
        if(this.spriteRender == null) this.spriteRender = GetComponent<SpriteRenderer>();
        this.spriteRender.sprite = sprite;
        this.spriteRender.color = color;
        gameObject.SetActive(setactive);
    }
}

using UnityEngine;

public class TextCurrentScore : BaseText
{
    private ScoreManager scoreManager;
    protected override void Start()
    {
        base.Start();
        this.scoreManager = GameObject.FindFirstObjectByType<ScoreManager>();
    }
    protected override void Update()
    {
        base.Update();
        SetText(this.scoreManager.CurentScore.ToString());
    }
}

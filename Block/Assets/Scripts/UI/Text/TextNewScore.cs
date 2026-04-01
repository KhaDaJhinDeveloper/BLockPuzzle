using UnityEngine;

public class TextNewScore : BaseText
{
    ScoreManager scoreManager;
    protected override void Start()
    {
        base.Start();
        this.scoreManager = GameObject.FindFirstObjectByType<ScoreManager>();
    }
    protected override void Update()
    {
        if (GameManager.Instance.newBestScore) 
            SetText("New Score: " + this.scoreManager.bestScore.ToString());
        else SetText("");
    }
}

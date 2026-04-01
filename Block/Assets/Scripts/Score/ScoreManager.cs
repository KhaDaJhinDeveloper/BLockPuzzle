using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int curentScore = 0;
    public int bestScore = 0;

    public void IncreaseScore(int score)
    {
        this.curentScore += score;
        if(this.curentScore > this.bestScore)
        {
            this.bestScore = this.curentScore;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sfx_NewScore);
            GameManager.Instance.newBestScore = true;
        }
    }
    public void Reset()
    {
        this.curentScore = 0;
        this.bestScore = 0;
    }
    public void Retry()
    {
        this.curentScore = 0;
    }
}

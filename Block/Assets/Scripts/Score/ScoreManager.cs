using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int curentScore = 0;
    private int bestScore = 0;
    public int CurentScore { get => curentScore;}
    public int BestScore { get => bestScore;}

    public void IncreaseScore(int score)
    {
        this.curentScore += score;
        if(this.curentScore > this.bestScore)
            this.bestScore = this.curentScore;
    }
    public void Reset()
    {
        this.curentScore = 0;
        this.bestScore = 0;
    }
}

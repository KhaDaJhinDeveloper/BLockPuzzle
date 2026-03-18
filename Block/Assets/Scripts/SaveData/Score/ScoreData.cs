[System.Serializable]
public class ScoreData
{
    public int currentScore;
    public int bestScore;
    public ScoreData(int currentScore, int bestScore)
    { 
        this.currentScore = currentScore; 
        this.bestScore = bestScore; 
    }
}

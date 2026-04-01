[System.Serializable]
public class ScoreData
{
    public int currentScore;
    public int bestScore;
    public ScoreData( int bestScore)
    { 
        this.bestScore = bestScore; 
    }
}

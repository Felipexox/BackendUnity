public class Score {

    private int mScore = 0;

    public void AddPoint(int pPoint)
    {
        MScore += pPoint;
    }

    public void ResetScore()
    {
        mScore = 0;
    }

    public int MScore
    {
        get
        {
            return mScore;
        }

        set
        {
            mScore = value;
        }
    }

}

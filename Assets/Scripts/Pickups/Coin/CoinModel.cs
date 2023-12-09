
public class CoinModel
{
    public int pointsGain { get; private set; }

    public CoinModel(CoinSO coinSO)
    {
        pointsGain = coinSO.pointsGain;
    }
}

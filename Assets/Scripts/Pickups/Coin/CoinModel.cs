
public class CoinModel
{
    public int coinPointsGain { get; private set; }

    private CoinSO coinSO;

    public CoinModel(CoinSO coinSO)
    {
        this.coinSO = coinSO;

        coinPointsGain = coinSO.coinPointsGain;
    }
}

using UnityEngine;

public class CoinPool : ObjectPoolGeneric<CoinView>
{
    private CoinView coinPrefab;

    public CoinView GetCoin(CoinView coinPrefab)
    {
        this.coinPrefab = coinPrefab;

        return GetItemFromPool();
    }

    protected override CoinView CreateItem()
    {
        CoinView coinView = Object.Instantiate(coinPrefab);
        return coinView;
    }
}

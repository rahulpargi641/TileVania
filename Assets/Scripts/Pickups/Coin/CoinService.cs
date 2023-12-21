using UnityEngine;

public class CoinService : MonoSingletonGeneric<CoinService>
{
    [SerializeField] CoinSO coinSO;

    private CoinPool coinPool = new CoinPool();

    // Start is called before the first frame update
    void Start()
    {
        CoinView.onCoinPickedUp += ReturnCoinToPool;
    }

    private void OnDestroy()
    {
        CoinView.onCoinPickedUp -= ReturnCoinToPool;
    }

    public void SpawnCoin(Vector2 spawnPointPos)
    {
        CoinView coinView = coinPool.GetCoin(coinSO.coinView);
        coinView.InitializeModel(coinSO);

        coinView.SetTransform(spawnPointPos);
        coinView.EnableCoin();
    }

    private void ReturnCoinToPool(CoinView coinView)
    {
        coinView.DisableCoin();
        coinPool.ReturnItem(coinView);
    }
}

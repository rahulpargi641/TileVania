using UnityEngine;

public class CoinService : MonoSingletonGeneric<CoinService>
{
    [SerializeField] CoinSO coinSO;

    private CoinPool coinPool;

    // Start is called before the first frame update
    void Start()
    {
        coinPool = new CoinPool();

        CoinView.onCoinPickedUp += ReturnCoinToPool;
    }

    private void OnDestroy()
    {
        CoinView.onCoinPickedUp -= ReturnCoinToPool;
    }

    public void SpawnCoin(Vector2 spawnPointPos)
    {
        CoinView coinPresenter = coinPool.GetCoin(coinSO.coinView);
        coinPresenter.InitializeModel(coinSO);
        coinPresenter.SetTransform(spawnPointPos);
        coinPresenter.EnableCoin();
    }

    private void ReturnCoinToPool(CoinView coinView)
    {
        coinView.DisableCoin();
        coinPool.ReturnItem(coinView);
    }
}

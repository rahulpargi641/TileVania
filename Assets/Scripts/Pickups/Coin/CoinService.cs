using UnityEngine;

public class CoinService : MonoSingletonGeneric<CoinService>
{
    [SerializeField] CoinSO coinSO;

    private CoinPool coinPool;

    // Start is called before the first frame update
    void Start()
    {
        coinPool = new CoinPool();
    }

    public void SpawnCoin(Vector2 spawnPointPos)
    {
        CoinModel coinModel = new CoinModel(coinSO);

        CoinPresenter coinPresenter = coinPool.GetCoin(coinSO.coinPresenter);
        coinPresenter.InitializeModel(coinModel);
        coinPresenter.SetTransform(spawnPointPos);
        coinPresenter.EnableCoin();
    }

    public void ReturnCoinToPool(CoinPresenter coinPresenter)
    {
        coinPresenter.DisableCoin();
        coinPool.ReturnItem(coinPresenter);
    }
}

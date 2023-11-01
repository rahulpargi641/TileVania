using UnityEngine;

public class CoinPool : ObjectPoolGeneric<CoinPresenter>
{
    private CoinPresenter presenter;

    public CoinPresenter GetCoin(CoinPresenter presenter)
    {
        this.presenter = presenter;

        return GetItemFromPool();
    }

    protected override CoinPresenter CreateItem()
    {
        CoinPresenter coinPresenter = Object.Instantiate(presenter);
        return coinPresenter;
    }
}

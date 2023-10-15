using UnityEngine;

public class ArrowPool : ObjectPoolGeneric<ArrowPresenter>
{
    private ArrowPresenter presenter;

    public ArrowPresenter GetArrow(ArrowPresenter presenter)
    {
        this.presenter = presenter;

        return GetItemFromPool();
    }

    protected override ArrowPresenter CreateItem()
    {
        ArrowPresenter arrowPresenter = Object.Instantiate(presenter);

        return arrowPresenter;
    }
}

using UnityEngine;

public class EnemyPool : ObjectPoolGeneric<EnemyPresenter>
{
    private EnemyPresenter presenter;

    public EnemyPresenter GetEnemy(EnemyPresenter presenter)
    {
        this.presenter = presenter;

        return GetItemFromPool();
    }

    protected override EnemyPresenter CreateItem()
    {
        EnemyPresenter arrowPresenter = Object.Instantiate(presenter);

        return arrowPresenter;
    }
}

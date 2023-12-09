using UnityEngine;

public class EnemyPool : ObjectPoolGeneric<EnemyView>
{
    private EnemyView enemyPrefab;

    public EnemyView GetEnemy(EnemyView enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;

        return GetItemFromPool();
    }

    protected override EnemyView CreateItem()
    {
        EnemyView arrowView = Object.Instantiate(enemyPrefab);

        return arrowView;
    }
}

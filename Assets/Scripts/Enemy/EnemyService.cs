using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>, IDamageable
{
    [SerializeField] EnemySO enemySO;

    private List<EnemyPresenter> enemyPresenters;
    private EnemyPool enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        enemyPresenters = new List<EnemyPresenter>();
        enemyPool = new EnemyPool();

        ArrowPresenter.onArrowHit += ReturnEnemyToPool;
    }

    private void OnDestroy()
    {
        ArrowPresenter.onArrowHit -= ReturnEnemyToPool;
    }

    public void SpawnEnemy(Vector3 spawnPointPos)
    {
        EnemyModel enemyModel = new EnemyModel(enemySO);
        EnemyPresenter enemyPresenter = enemyPool.GetEnemy(enemySO.enemyPresenter);
        enemyPresenter.InitialzeModel(enemyModel);
        enemyPresenter.SetTransform(spawnPointPos);
        enemyPresenter.EnableEnemy();

        enemyPresenters.Add(enemyPresenter);
    }

    private void ReturnEnemyToPool(EnemyPresenter enemyPresenter)
    {
        enemyPresenter.DisableEnemy();
        enemyPool.ReturnItem(enemyPresenter);
    }
}

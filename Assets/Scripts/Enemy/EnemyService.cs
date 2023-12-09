using UnityEngine;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    [SerializeField] EnemySO enemySO;

    private EnemyPool enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new EnemyPool();

        ArrowView.onArrowHit += ReturnEnemyToPool;
    }

    private void OnDestroy()
    {
        ArrowView.onArrowHit -= ReturnEnemyToPool;
    }

    public void SpawnEnemy(Vector3 spawnPointPos)
    {
        EnemyView enemyView = enemyPool.GetEnemy(enemySO.enemyView);
        enemyView.InitialzeModel(enemySO);
        enemyView.SetTransform(spawnPointPos);
        enemyView.EnableEnemy();
    }

    private void ReturnEnemyToPool(EnemyView enemyView)
    {
        enemyView.DisableEnemy();
        enemyPool.ReturnItem(enemyView);
    }
}

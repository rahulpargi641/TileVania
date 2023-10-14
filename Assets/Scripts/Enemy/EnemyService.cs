using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    private List<EnemyPresenter> enemyPresenters;

    // Start is called before the first frame update
    void Start()
    {
        enemyPresenters = new List<EnemyPresenter>();
    }

    public void SpawnEnemy(Transform spawnPoint)
    {
        EnemyModel enemyModel = new EnemyModel(enemySO);
        EnemyPresenter enemyPresenter = Instantiate(enemySO.enemyPresenter, spawnPoint.position, spawnPoint.rotation);
        enemyPresenter.InitialzeModel(enemyModel);

        enemyPresenters.Add(enemyPresenter);
    }
}

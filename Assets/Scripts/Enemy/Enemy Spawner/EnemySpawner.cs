using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    //[SerializeField] EnemyPresenter[] enemyViews;

    private bool areEnemiesSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        if (areEnemiesSpawned) return;

        areEnemiesSpawned = true;

        foreach (Transform spawnPoint in spawnPoints)
        {
            EnemyService.Instance.SpawnEnemy(spawnPoint.position);
        }
    }
}

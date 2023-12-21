using UnityEngine;

public class EnemySpawner : MonoBehaviour // Multiple EnemySpawner exists through out the level in different areas.
{
    [SerializeField] Transform[] spawnPoints; // Spawn points for the enemies, when player enters the EnemySpawner trigger, all the enemies in that area will be spawned.

    private bool areEnemiesSpawned = false; // For spawning enemies only once when player enters the EnemySpwner trigger for the first time. if player enters the trigger second time, it will not spawn the enemies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>())
            SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (areEnemiesSpawned) return;

        foreach (Transform spawnPoint in spawnPoints)
            EnemyService.Instance.SpawnEnemy(spawnPoint.position);

        areEnemiesSpawned = true;
    }
}

using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    private bool areCoinsSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            SpawnCoins();
        }
    }

    public void SpawnCoins()
    {
        if (areCoinsSpawned) return;

        areCoinsSpawned = true;

        foreach (Transform spawnPoint in spawnPoints)
        {
            CoinService.Instance.SpawnCoin(spawnPoint.position);
        }
    }
}

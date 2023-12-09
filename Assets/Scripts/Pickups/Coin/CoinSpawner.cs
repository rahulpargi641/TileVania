using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    private bool areCoinsSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>())
        {
            SpawnCoins();
        }
    }

    public void SpawnCoins()
    {
        if (areCoinsSpawned) return;

        areCoinsSpawned = true;

        foreach (Transform spawnPoint in spawnPoints) // Performance difference is negligible between for and foreach. since foreach provides more readability so its good idea to use foreach
                                                      //  where you don't need to access the elements by index.
        {
            CoinService.Instance.SpawnCoin(spawnPoint.position);
        }
    }
}

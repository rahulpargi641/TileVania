using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] PlayerPresenter playerPresenter;

    void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerModel playerModel = new PlayerModel();
        //playerPresenter = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);        
        playerPresenter.InitializeModel(playerModel);
    }
}

using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] PlayerPresenter playerPresenter;

    void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerModel playerModel = new PlayerModel(playerSO);
        //playerPresenter = Instantiate(playerSO.playerPresenter, spawnPoint.position, spawnPoint.rotation);        
        playerPresenter.InitializeModel(playerModel);
    }
}

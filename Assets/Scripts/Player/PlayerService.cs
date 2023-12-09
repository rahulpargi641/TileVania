using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] PlayerView playerView;

    protected override void Awake()
    {
        base.Awake();

        SpawnPlayer();
    }

    private void SpawnPlayer() // Game Service will spawn the player in future, maybe you want to spawn the player at dying location everytime player dies until you run of lives.
    {
        // playerView = Instantiate(playerSO.playerView, spawnPoint.position, spawnPoint.rotation);        
        playerView.InitializeModel(playerSO);
    }
}

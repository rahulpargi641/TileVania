using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private PlayerView playerView;

    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();

        SpawnPlayer();
    }

    private void SpawnPlayer() // Game Service will spawn the player in future, eg. when you want to keep spawn the player at dying location everytime player dies until you run of lives.
    {
        playerController = new PlayerController(playerView, playerSO);
    }
}

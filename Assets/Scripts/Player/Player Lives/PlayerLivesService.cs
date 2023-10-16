using UnityEngine;

public class PlayerLivesService : MonoSingletonGeneric<PlayerLivesService>
{
    [SerializeField] PlayerLivesPresenter playerLivesPresenter;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPresenter.onPlayerLivesChange += UpdatePlayerLivesUI;
    }

    void OnDestroy()
    {
        PlayerPresenter.onPlayerLivesChange -= UpdatePlayerLivesUI;
    }

    public void UpdatePlayerLivesUI(int currentLives)
    {
        playerLivesPresenter.UpdatePlayerLivesUI(currentLives);
    }
}

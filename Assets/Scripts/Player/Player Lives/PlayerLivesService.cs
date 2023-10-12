using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesService : MonoSingletonGeneric<PlayerLivesService>
{
    [SerializeField] PlayerLivesPresenter playerLivesPresenter;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPresenter.onPlayerDeath += DecreaseLives;
    }

    void OnDestroy()
    {
        PlayerPresenter.onPlayerDeath -= DecreaseLives;
    }

    public void DecreaseLives()
    {
        playerLivesPresenter.DecreaseLives();
    }
}

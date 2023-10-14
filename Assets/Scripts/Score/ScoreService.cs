using UnityEngine;

public class ScoreService : MonoSingletonGeneric<ScoreService>
{
    [SerializeField] ScorePresenter scorePresenter;

    // Start is called before the first frame update
    void Start()
    {
        CoinPresenter.onCoinPickup += IncreaseScore;
    }

    private void OnDestroy()
    {
        CoinPresenter.onCoinPickup -= IncreaseScore;
    }

    // Called when onCoinPickup event is invoked
    public void IncreaseScore(int pointsGain)
    {
        //scorePresenter.IncreaseScore(pointsGain);
    }
}

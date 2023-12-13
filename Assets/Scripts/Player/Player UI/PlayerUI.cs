using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    private void Start()
    {
        PlayerView.onPlayerLivesChange += UpdatePlayerLivesUI;
        CoinView.onCoinPickup += IncreaseScore;
    }

    private void OnDestroy()
    {
        PlayerView.onPlayerLivesChange -= UpdatePlayerLivesUI;
        CoinView.onCoinPickup -= IncreaseScore;
    }

    public void UpdatePlayerLivesUI(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }

    public void IncreaseScore(int pointsGain)
    {
        currentScore += pointsGain;
        scoreText.text = currentScore.ToString();
    }
}

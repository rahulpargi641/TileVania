using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        PlayerView.onPlayerLivesChange += UpdatePlayerLivesUI;
        CoinView.onCoinPickup += IncreaseScore;
    }

    void OnDestroy()
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

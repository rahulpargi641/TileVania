using UnityEngine;
using TMPro;

public class ScorePresenter : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseScore(int pointsGain)
    {
        currentScore += pointsGain;
        scoreText.text = currentScore.ToString();
    }
}

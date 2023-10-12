using TMPro;
using UnityEngine;

public class PlayerLivesPresenter : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    private int currentLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    public void DecreaseLives()
    {
        currentLives--;
        livesText.text = currentLives.ToString();
    }
}

using TMPro;
using UnityEngine;

public class PlayerLivesPresenter : MonoBehaviour
{
    private TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdatePlayerLivesUI(int currentLives)
    {
        livesText.text = currentLives.ToString();
    }
}

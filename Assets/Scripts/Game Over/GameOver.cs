using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button playAgainButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        InitializeButtons();
    }

    private void Start()
    {
        AudioService.Instance.StopSound(SoundType.BackgroundMusic);
    }

    private void InitializeButtons()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayAgain()
    {
        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 2;
        if (prevSceneIndex < 1)
            prevSceneIndex = 1;

        SceneManager.LoadScene(prevSceneIndex);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
        if (Application.isPlaying)
            Application.Quit(); 

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
        AudioService.Instance.PlaySound(SoundType.ButtonClick);
    }
}

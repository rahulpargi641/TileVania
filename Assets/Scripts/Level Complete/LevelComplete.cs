using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] Button playAgainButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        AudioService.Instance.StopSound(SoundType.BackgroundMusic);
    }

    private void PlayAgain()
    {
        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (prevSceneIndex < 1)
            prevSceneIndex = 1;

        SceneManager.LoadScene(prevSceneIndex);
    }

    private void QuitGame()
    {
        Application.Quit(); 

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
        AudioService.Instance.PlaySound(SoundType.ButtonClick);
    }
}

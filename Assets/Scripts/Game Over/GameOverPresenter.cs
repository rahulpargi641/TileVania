using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}

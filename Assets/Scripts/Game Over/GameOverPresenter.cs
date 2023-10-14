using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    private GameOverModel model;

    private void Awake()
    {
        model = new GameOverModel();

        startButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPresenter.onPlayerDeath += GameOver;
    }

    private void OnDestroy()
    {
        PlayerPresenter.onPlayerDeath -= GameOver;
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

    private void GameOver()
    {
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSecondsRealtime(model.LevelLoadDelay);

        SceneManager.LoadScene(model.GameOverSceneName);
    }
}

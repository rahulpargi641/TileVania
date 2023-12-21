using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private LevelModel model;

    private void Awake()
    {
        model = new LevelModel();

        InitializeButtons();

        pauseScreen.SetActive(false); // Disable Pause Screen
    }

    private void Start()
    {
        PlayerController.onPlayerDeath += GameOver;
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDeath -= GameOver;
    }

    private void Update()
    {
        HandlePauseInput();
    }

    private void InitializeButtons()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (model.IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        model.IsPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; // Pause time
    }

    private void ResumeGame()
    {
        model.IsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Resume time
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
#endif
        AudioService.Instance.PlaySound(SoundType.ButtonClick);
    }

    private void GameOver()
    {
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSecondsRealtime(model.LevelLoadDelay);

        int gameOverSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(gameOverSceneIndex);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private bool isPaused = false;

    private LevelModel model;

    private void Awake()
    {
        model = new LevelModel();

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        pauseScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPresenter.onPlayerDeath += GameOver;

        //AudioService.Instance.PlaySound(SoundType.BackgroundMusic);
    }

    private void OnDestroy()
    {
        PlayerPresenter.onPlayerDeath -= GameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; // Pause time

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Resume time

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit(); // Quit the game directly
        }
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



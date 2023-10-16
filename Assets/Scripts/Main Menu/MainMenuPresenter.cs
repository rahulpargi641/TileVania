using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button howToPlay;
    [SerializeField] GameObject instructionsScreen;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        howToPlay.onClick.AddListener(ShowInstructionScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideInstructionsScreen();
        }
    }

    private void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ShowInstructionScreen()
    {
        instructionsScreen.SetActive(true);
    }

    void HideInstructionsScreen()
    {
        if (instructionsScreen.activeSelf)
        {
            instructionsScreen.SetActive(false);
        }
    }
}

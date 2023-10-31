using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletePresenter : MonoBehaviour
{
    private LevelCompleteModel model;

    private void Awake()
    {
        model = new LevelCompleteModel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerPresenter>())
            StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        AudioService.Instance.PlaySound(SoundType.LevelComplete);
        yield return new WaitForSecondsRealtime(model.LevelLoadDelay);

        SceneManager.LoadScene(model.GameCompleteSceneName);
    }
}

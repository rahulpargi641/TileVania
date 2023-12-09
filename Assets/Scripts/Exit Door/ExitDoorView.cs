using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorView : MonoBehaviour
{
    private ExitDoorModel model;

    private void Awake()
    {
        model = new ExitDoorModel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerView>())
            StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        AudioService.Instance.PlaySound(SoundType.LevelComplete);
        yield return new WaitForSecondsRealtime(model.LevelLoadDelay);

        SceneManager.LoadScene(model.GameCompleteSceneName);
    }
}

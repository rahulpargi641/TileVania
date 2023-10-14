
public class GameOverModel
{
    public float LevelLoadDelay { get; private set; }
    public string GameOverSceneName { get; private set; } = "GameOver";

    public GameOverModel()
    {
        LevelLoadDelay = 1f;
    }
}

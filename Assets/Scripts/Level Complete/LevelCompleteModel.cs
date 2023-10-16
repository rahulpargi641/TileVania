
public class LevelCompleteModel
{
    public float LevelLoadDelay { get; private set; }
    public string GameCompleteSceneName { get; private set; } = "GameComplete";


    public LevelCompleteModel()
    {
        LevelLoadDelay = 1f;
    }
}

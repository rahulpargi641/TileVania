
public class ExitDoorModel
{
    public float LevelLoadDelay { get; private set; }
    public string GameCompleteSceneName { get; private set; } = "GameComplete";

    public ExitDoorModel()
    {
        LevelLoadDelay = 1f;
    }
}

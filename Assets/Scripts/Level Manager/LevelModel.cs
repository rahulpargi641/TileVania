
public class LevelModel
{
    public float LevelLoadDelay { get; private set; }
    public bool IsPaused { get; set; }
    public LevelModel()
    {
        LevelLoadDelay = 2f;
    }
}

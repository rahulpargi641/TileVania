
public class ArrowModel
{
    public float ArrowSpeed { get; private set; }

    public ArrowModel(ArrowSO arrowSO)
    {
        ArrowSpeed = arrowSO.speed;
    }
}


public class ArrowModel
{
    public float ArrowSpeed { get; private set; }
    public float XSpeed { get; set; }

    private ItemSO arrowConfig;

    public ArrowModel(ItemSO arrowConfig)
    {
        this.arrowConfig = arrowConfig;

        ArrowSpeed = arrowConfig.itemSpeed;
    }
}

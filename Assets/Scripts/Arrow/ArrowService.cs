using UnityEngine;

public class ArrowService : MonoSingletonGeneric<ArrowService>
{
    [SerializeField] ArrowSO arrowSO;
    private ArrowPool arrowPool;

    // Start is called before the first frame update
    void Start()
    {
        arrowPool = new ArrowPool();
    }

    public void SpawnArrow(Vector2 spawnPointPos, Vector2 spawnPointScale)
    {
        ArrowModel arrowModel = new ArrowModel(arrowSO);

        ArrowPresenter arrowPresenter = arrowPool.GetArrow(arrowSO.arrowPresenter);
        arrowPresenter.InitializeModel(arrowModel);
        arrowPresenter.SetTransform(spawnPointPos, spawnPointScale);
        arrowPresenter.EnableArrow();
    }

    public void ReturnArrowToPool(ArrowPresenter arrowPresenter)
    {
        arrowPresenter.DisableArrow();
        arrowPool.ReturnItem(arrowPresenter);
    }
}

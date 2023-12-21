using UnityEngine;

public class ArrowService : MonoSingletonGeneric<ArrowService>
{
    [SerializeField] ArrowSO arrowSO;

    private ArrowPool arrowPool = new ArrowPool();

    // Start is called before the first frame update
    private void Start()
    {
        ArrowView.onArrowCollided += ReturnArrowToPool;
    }

    private void OnDestroy()
    {
        ArrowView.onArrowCollided -= ReturnArrowToPool;
    }

    public void SpawnArrow(Vector2 spawnPointPos, Vector2 spawnPointScale)
    {
        ArrowView arrowView = arrowPool.GetArrow(arrowSO.arrowPrefab);
        arrowView.InitializeModel(arrowSO);

        arrowView.SetTransform(spawnPointPos, spawnPointScale);
        arrowView.EnableArrow();
    }

    private void ReturnArrowToPool(ArrowView arrowView)
    {
        arrowView.DisableArrow();
        arrowPool.ReturnItem(arrowView);
    }
}

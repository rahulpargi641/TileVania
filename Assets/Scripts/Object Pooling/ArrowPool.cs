using UnityEngine;

public class ArrowPool : ObjectPoolGeneric<ArrowView>
{
    private ArrowView arrowPrefab;

    public ArrowView GetArrow(ArrowView arrowPrefab)
    {
        this.arrowPrefab = arrowPrefab;

        return GetItemFromPool();
    }

    protected override ArrowView CreateItem()
    {
        ArrowView arrowView = Object.Instantiate(arrowPrefab);

        return arrowView;
    }
}

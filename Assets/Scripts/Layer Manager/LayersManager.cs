using UnityEngine;

public class LayersManager : MonoSingletonGeneric<LayersManager>
{
    [field:SerializeField] public LayerMask PlatformLayer { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask ClimbingLayer { get; private set; }
    [field: SerializeField] public LayerMask WaterLayer { get; private set; }
    [field: SerializeField] public LayerMask HazardLayer { get; private set; }
    [field: SerializeField] public LayerMask BouncyMashorromLayer { get; private set; }
}

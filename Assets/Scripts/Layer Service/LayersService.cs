using UnityEngine;

public class LayersService : MonoSingletonGeneric<LayersService>
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask climbingLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask hazardLayer;
    public LayerMask PlatformLayer => platformLayer;
    public LayerMask EnemyLayer => enemyLayer;
    public LayerMask ClimbingLayer => climbingLayer;
    public LayerMask WaterLayer => waterLayer;
    public LayerMask HazardLayer => hazardLayer;
}

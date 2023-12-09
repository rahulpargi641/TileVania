using UnityEngine;

public class EnemyModel
{
    public float MoveSpeed { get; set; } 
    public EnemyModel(EnemySO enemSO)
    {
        MoveSpeed = enemSO.moveSpeed;
    }
}

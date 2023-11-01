using UnityEngine;

public class EnemyModel
{
    public float MoveSpeed { get; set; }
    public Vector2 deathKick { get; private set; }

    private EnemySO enemySO;

    public EnemyModel(EnemySO enemySO)
    {
        this.enemySO = enemySO;

        MoveSpeed = enemySO.moveSpeed;
        deathKick = enemySO.deathKick;
    }
}

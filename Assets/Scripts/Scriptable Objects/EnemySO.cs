using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Player Info")]
    public new string name;
    public EnemyPresenter enemyPresenter;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public Vector2 deathKick = new Vector2(5f, 5f);


    [Header("Animation Names")]
    public string idleAnimName = "Idle";
    public string runAnimName = "Run";
    public string shootAnimName = "attack";
    public string deadAnimName = "Dead";
}

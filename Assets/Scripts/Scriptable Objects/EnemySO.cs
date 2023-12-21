using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Info")]
    public new string name;

    public EnemyView enemyView;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public Vector2 deathKick = new Vector2(5f, 5f);
}

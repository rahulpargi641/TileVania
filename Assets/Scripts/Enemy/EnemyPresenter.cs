using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private EnemyModel model;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        model = new EnemyModel();
    }

    void Update()
    {
        rigidBody2D.velocity = new Vector2(model.MoveSpeed, 0f);
    }

    public void InitialzeModel(EnemyModel model)
    {
        this.model = model;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        model.MoveSpeed = -model.MoveSpeed;
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody2D.velocity.x)), 1f);
    }
}

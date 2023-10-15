using UnityEngine;

public class ArrowPresenter : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private ArrowModel model;

    public void InitializeModel(ArrowModel model)
    {
        this.model = model;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        model.XSpeed = transform.localScale.y * model.ArrowSpeed;
        rigidbody.velocity = new Vector2(model.XSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyPresenter enemy = collision.GetComponent<EnemyPresenter>();
        if (enemy)
            Destroy(enemy.gameObject);

        ArrowService.Instance.ReturnArrowToPool(this);
    }

    // If arrow collides with wall or other object other than enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ArrowService.Instance.ReturnArrowToPool(this);
    }

    public void SetTransform(Vector2 spawnPointPos, Vector2 spawnPointScale)
    {
        transform.position = spawnPointPos;

        Vector2 localScale = transform.localScale;
        transform.localScale = new Vector2(localScale.x, Mathf.Sign(spawnPointScale.x));
    }

    public void EnableArrow()
    {
        gameObject.SetActive(true);
    }

    internal void DisableArrow()
    {
        gameObject.SetActive(false);
    }
}

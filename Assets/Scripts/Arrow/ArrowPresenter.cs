using UnityEngine;

public class ArrowPresenter : MonoBehaviour
{
    [SerializeField] ItemSO arrowConfig;
    private Rigidbody2D rigidbody;

    private ArrowModel model;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        model = new ArrowModel(arrowConfig);
        model.XSpeed = transform.localScale.y * model.ArrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(model.XSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyPresenter enemy = collision.GetComponent<EnemyPresenter>();
        if (enemy)
            Destroy(enemy.gameObject);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}

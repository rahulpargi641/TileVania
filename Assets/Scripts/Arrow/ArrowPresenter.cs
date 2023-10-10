using UnityEngine;

public class ArrowPresenter : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private ArrowModel model;


    // Start is called before the first frame update
    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        model = new ArrowModel();
        model.xSpeed = transform.localScale.y * model.ArrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(model.xSpeed, 0f);
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

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowView : MonoBehaviour
{
    public static event Action<EnemyView> onArrowHit; // I did not create separate 'EventService' for handling events because it helps prevent bugs. Using a singleton to invoke events can easily lead to bugs since events can be invoked from anywhere. If I have 50 scripts, they could invoke events from all 50 scripts. That's why it's better to have events in the script that will invoked them.

    public static event Action<ArrowView> onArrowCollided; 
    
    private Rigidbody2D rigidbody;
    private ArrowModel model;

    public void InitializeModel(ArrowSO arrowSO)
    {
        model = new ArrowModel(arrowSO);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = transform.localScale.y * model.ArrowSpeed;
        rigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyView enemyView = collision.GetComponent<EnemyView>();
        if (enemyView)
        {
            onArrowHit?.Invoke(enemyView);
            onArrowCollided?.Invoke(this);
            AudioService.Instance.PlaySound(SoundType.EnemyDeath);
        }
    }

    // If arrow collides with wall or other object other than enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onArrowCollided?.Invoke(this);
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

    public void DisableArrow()
    {
        gameObject.SetActive(false);
    }
}

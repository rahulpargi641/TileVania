using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);

    private Rigidbody2D rigidBody2D;
    private CapsuleCollider2D bodyCapsuleCollider2D;
    private Animator animator;

    private bool alive = true;
    private bool canJump = true;
    private bool shootingAnimationEnd = false;
    private float gravityScaleAtStart;
    private Vector2 moveInput;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        bodyCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        gravityScaleAtStart = rigidBody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;

        Run();
        ClimbLadder();
        Shooting();
        Die();
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = playerVelocity;

        bool playerHasSpeedX = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasSpeedX)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody2D.velocity.x), 1f);

        animator.SetBool("Running", playerHasSpeedX);
    }

    void ClimbLadder()
    {
        if (!bodyCapsuleCollider2D.IsTouchingLayers(LayersSingleton.Instance.ClimbingLayer))
        {
            rigidBody2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("Climbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidBody2D.velocity.x, moveInput.y * climbSpeed);
        rigidBody2D.velocity = climbVelocity;
        rigidBody2D.gravityScale = 0f;

        bool playerHasSpeedY = rigidBody2D.velocity.y > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasSpeedY);
    }

    private void Die()
    {
        if (bodyCapsuleCollider2D.IsTouchingLayers(LayersSingleton.Instance.EnemyLayer))
        {
            alive = false;
            rigidBody2D.velocity = deathKick;
            animator.SetTrigger("Die");
            return;
        }

        if (bodyCapsuleCollider2D.IsTouchingLayers(LayersSingleton.Instance.HazardLayer) || bodyCapsuleCollider2D.IsTouchingLayers(LayersSingleton.Instance.WaterLayer))
        {
            alive = false;
            animator.SetTrigger("Die");
        }
    }

    private void Shooting()
    {
        if (shootingAnimationEnd)
        {
            animator.SetBool("Shooting", false);
            shootingAnimationEnd = false;
        }
    }

    void OnMove(InputValue value)
    {
        if (!alive) return;

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!alive) return;

        if (value.isPressed)
        {
            if(canJump)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
                animator.SetBool("Jumping", true);
            }
        }
    }

    void OnFire(InputValue value)
    {
        if (!alive) return;

        if(value.isPressed)
        {
            animator.SetBool("Shooting", true);
        }
    }

    public void ShootingAnimationEnded()
    {
        shootingAnimationEnd = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bodyCapsuleCollider2D.IsTouchingLayers(LayersSingleton.Instance.PlatformLayer))
        {
            canJump = true;
            animator.SetBool("Jumping", false);
        }
        else
        {
            canJump = false;
        }
    }
}

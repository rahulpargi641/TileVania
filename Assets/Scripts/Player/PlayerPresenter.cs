using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPresenter : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D bodyCapsuleCollider;
    private Animator animator;

    private Vector2 moveInput;
    private PlayerModel model;

    public static event Action onPlayerDeath;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        model.GravityScaleAtStart = rigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!model.IsAlive) return;

        ProcessRunning();
        ProcessClimbingLadder();
        ProcessShooting();
        ProcessDeath();
    }

    public void InitializeModel(PlayerModel model)
    {
        this.model = model;
    }

    private void ProcessRunning()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * model.RunSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasSpeedX = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasSpeedX)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);

        animator.SetBool("Running", playerHasSpeedX);
    }

    void ProcessClimbingLadder()
    {
        if (!bodyCapsuleCollider.IsTouchingLayers(LayersService.Instance.ClimbingLayer))
        {
            rigidBody.gravityScale = model.GravityScaleAtStart;
            animator.SetBool("Climbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * model.ClimbSpeed);
        rigidBody.velocity = climbVelocity;
        rigidBody.gravityScale = 0f;

        bool playerHasSpeedY = rigidBody.velocity.y > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasSpeedY);
    }

    private void ProcessDeath()
    {
        if (bodyCapsuleCollider.IsTouchingLayers(LayersService.Instance.EnemyLayer))
        {
            model.IsAlive = false;
            rigidBody.velocity = model.DeathKick;

            onPlayerDeath?.Invoke();

            animator.SetTrigger("Die");
            return;
        }

        if (bodyCapsuleCollider.IsTouchingLayers(LayersService.Instance.HazardLayer) || bodyCapsuleCollider.IsTouchingLayers(LayersService.Instance.WaterLayer))
        {
            model.IsAlive = false;

            onPlayerDeath?.Invoke();

            animator.SetTrigger("Die");
        }

        if(model.IsAlive)
        {
            // Load Game Over Screen via invoking the event
            // Destroy game session
        }
    }

    public void TakeLife()
    {
        model.Lives--;
        // Load the current scene again
    }

    private void ProcessShooting()
    {
        if (model.ShootingAnimationEnd)
        {
            animator.SetBool("Shooting", false);
            model.ShootingAnimationEnd = false;
        }
    }

    void OnMove(InputValue value)
    {
        if (!model.IsAlive) return;

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!model.IsAlive) return;

        if (value.isPressed)
        {
            if(model.CanJump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, model.JumpSpeed);
                animator.SetBool("Jumping", true);
                model.CanJump = false;
            }
        }
    }

    void OnFire(InputValue value)
    {
        if (!model.IsAlive) return;

        if(value.isPressed)
        {
            animator.SetBool("Shooting", true);
        }
    }

    public void ShootingAnimationEnded()
    {
        model.ShootingAnimationEnd = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bodyCapsuleCollider.IsTouchingLayers(LayersService.Instance.PlatformLayer))
        {
            model.CanJump = true;
            animator.SetBool("Jumping", false);
        }
    }
}

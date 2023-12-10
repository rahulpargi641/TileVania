using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{ 
    [SerializeField] Transform arrowSpawnPoint;

    public static event Action<int> onPlayerLivesChange;
    public static event Action onPlayerDeath;

    private Rigidbody2D rigidBody;
    private CapsuleCollider2D bodyCapsuleCollider;
    private Animator animator;

    private Vector2 moveInput;

    private PlayerModel model;

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
        ProcessPlayerDeath();
    }

    public void InitializeModel(PlayerSO playerSO)
    {
        model = new PlayerModel(playerSO);
    }

    private void ProcessRunning()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * model.RunSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        PlayerRunAnimation();
    }

    private void PlayerRunAnimation()
    {
        bool playerHasSpeedX = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasSpeedX)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);

        animator.SetBool(model.RunBoolName, playerHasSpeedX);
    }

    void ProcessClimbingLadder()
    {
        if (!bodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.ClimbingLayer))
        {
            rigidBody.gravityScale = model.GravityScaleAtStart;
            animator.SetBool(model.ClimbBoolName, false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * model.ClimbSpeed);
        rigidBody.velocity = climbVelocity;
        rigidBody.gravityScale = 0f;

        PlayerClimbAnimation();
    }

    private void PlayerClimbAnimation()
    {
        bool playerHasSpeedY = rigidBody.velocity.y > Mathf.Epsilon;
        animator.SetBool(model.ClimbBoolName, playerHasSpeedY);
    }

    private void ProcessPlayerDeath()
    {
        if (bodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.HazardLayer) || bodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.WaterLayer))
        {
            PlayerDead();
        }
    }

    private void DecreaseLives()
    {
        model.Lives--;

        if(model.Lives >= 0)
            onPlayerLivesChange?.Invoke(model.Lives);

        if (model.Lives <= 0)
            PlayerDead();
    }

    private void PlayerDead()
    {
        model.IsAlive = false;

        onPlayerDeath?.Invoke();

        animator.SetTrigger(model.DeadTriggerName);
        AudioService.Instance.PlaySound(SoundType.Hurt);
    }

    // called via animation event
    private void ProcessShooting()
    {
        if (model.ShootingAnimationEnd)
        {
            animator.SetBool(model.ShootBoolName, false);
            model.ShootingAnimationEnd = false;
        }
    }

    // Input System message
    void OnMove(InputValue value)
    {
        if (!model.IsAlive) return;

        moveInput = value.Get<Vector2>();
    }

    // Input System message
    void OnJump(InputValue value)
    {
        if (!model.IsAlive) return;

        if (value.isPressed)
        {
            if(model.IsGrounded)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, model.JumpSpeed);
                animator.SetBool(model.JumpBoolName, true);
                model.IsGrounded = false;
                AudioService.Instance.PlaySound(SoundType.Jump);
            }
        }
    }

    // Input System message
    void OnFire(InputValue value)
    {
        if (!model.IsAlive) return;

        if(value.isPressed)
        {
            animator.SetBool(model.ShootBoolName, true);
        }
    }

    // called via animation event
    public void ShootArrow()  
    {
        ArrowService.Instance.SpawnArrow(arrowSpawnPoint.position, transform.localScale);
        AudioService.Instance.PlaySound(SoundType.ArrowShooting);
    }

    // called via animaiton event
    public void ShootingAnimationEnded()
    {
        model.ShootingAnimationEnd = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyView>())
        {
            animator.SetBool(model.HurtBoolName, true);
            rigidBody.velocity = model.pushVelocity;
            AudioService.Instance.PlaySound(SoundType.Hurt);
            DecreaseLives();
        }

        if (bodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.PlatformLayer))
        {
            animator.SetBool(model.JumpBoolName, false);
            model.IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool(model.HurtBoolName, false);
    }

    public void PlayFootStepSound()
    {
        if(model.IsGrounded)
            AudioService.Instance.PlaySound(SoundType.Footsteps);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    public Rigidbody2D RigidBody { get; private set; }
    public CapsuleCollider2D BodyCapsuleCollider { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public bool ShootingAnimationEnd { get; set; }
    public PlayerController Controller { private get; set; }
    public Transform ArrowSpawnPoint => arrowSpawnPoint;

    [SerializeField] private Transform arrowSpawnPoint;
    private Animator animator;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        BodyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Controller.GravityScaleAtStart = RigidBody.gravityScale;
    }

    private void Update()
    {
        Controller.ProcessPlayerMovement(); // Process Player Running, Jumping and Climbing 

        HandlePlayerDeath();
    }

    public void RunAnimation()
    {
        bool playerHasSpeedX = FlipPlayer();

        animator.SetBool(Controller.RunBoolName, playerHasSpeedX);
    }

    private bool FlipPlayer()
    {
        bool playerHasSpeedX = Mathf.Abs(RigidBody.velocity.x) > Mathf.Epsilon;

        transform.localScale = playerHasSpeedX ? new Vector2(Mathf.Sign(RigidBody.velocity.x), 1f)
                                               : transform.localScale;
        return playerHasSpeedX;
    }

    public void ClimbAnimation(bool hasSpeedY)
    {
        animator.SetBool(Controller.ClimbBoolName, hasSpeedY);
    }

    public void JumpAnimation()
    {
        animator.SetBool(Controller.JumpBoolName, true);
    }

    public void StopShootingAnimation()
    {
        animator.SetBool(Controller.ShootBoolName, false);
    }

    // Input System message (custom input action)
    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    // Input System message (custom input action)
    void OnJump(InputValue value)
    {
        if (value.isPressed)
            Controller.ProcessJump();
    }

    // Input System message (custom input action)
    void OnFire(InputValue value)
    {
        if (!Controller.CanShoot) return;

        if (value.isPressed)
        {
            animator.SetBool(Controller.ShootBoolName, true); // start shooting animation
        }
    }

    // called via animation event
    public void ShootArrow()
    {
        Controller.ShootArrow();
    }

    // called via animation event
    public void ShootingAnimationEnded()
    {
        ShootingAnimationEnd = true;
    }

    private void HandlePlayerDeath()
    {
        if (BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.HazardLayer) ||
            BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.WaterLayer))
        {
            Controller.ProcessDeath();

            animator.SetTrigger(Controller.DeadTriggerName); // Plays Death animation
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyView>())
            ProcessEnemyCollision();

        if (BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.PlatformLayer))
            ProcessPlatformCollision();
    }

    private void ProcessEnemyCollision()
    {
        animator.SetBool(Controller.HurtBoolName, true); // Plays Hurt animation

        RigidBody.velocity = Controller.PushVelocity; // Pushes the player on collision with enemy
        Controller.DecreaseLives();

        AudioService.Instance.PlaySound(SoundType.Hurt);
    }

    private void ProcessPlatformCollision() // when player comes back on the ground after jumping
    {
        animator.SetBool(Controller.JumpBoolName, false); // Stops Jump animation
        Controller.IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool(Controller.HurtBoolName, false); // Stops Hurt animation
    }

    public void PlayFootStepSound()
    {
        if (Controller.IsGrounded)
            AudioService.Instance.PlaySound(SoundType.Footsteps);
    }
}

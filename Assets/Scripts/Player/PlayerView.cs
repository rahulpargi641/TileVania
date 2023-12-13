using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    public static event Action<int> onPlayerLivesChange;
    public static event Action onPlayerDeath;

    public Rigidbody2D RigidBody { get; private set; }
    public CapsuleCollider2D BodyCapsuleCollider { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public PlayerController Controller { get; set; }

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
        Controller.Model.GravityScaleAtStart = RigidBody.gravityScale;
    }

    private void Update()
    {
        if (!Controller.Model.IsAlive) return;

        Controller.ProcessPlayerMovement(); // Process Player Running, Jumping and Climbing 
        ProcessPlayerDeath();
    }

    public void PlayerRunAnimation()
    {
        bool playerHasSpeedX = Mathf.Abs(RigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasSpeedX)
            transform.localScale = new Vector2(Mathf.Sign(RigidBody.velocity.x), 1f);

        animator.SetBool(Controller.Model.RunBoolName, playerHasSpeedX);
    }

    public void PlayerClimbAnimation(bool hasSpeedY)
    {
        animator.SetBool(Controller.Model.ClimbBoolName, hasSpeedY);
    }

    public void StopShootingAnimation()
    {
        animator.SetBool(Controller.Model.ShootBoolName, false);
    }

    // Input System message (custom input action)
    void OnMove(InputValue value)
    {
        if (!Controller.Model.IsAlive) return;

        MoveInput = value.Get<Vector2>();
    }

    // Input System message (custom input action)
    void OnJump(InputValue value)
    {
        if (!Controller.Model.IsAlive) return;

        if (value.isPressed)
        {
            if (Controller.Model.IsGrounded)
            {
                RigidBody.velocity = new Vector2(RigidBody.velocity.x, Controller.Model.JumpSpeed);

                animator.SetBool(Controller.Model.JumpBoolName, true);

                Controller.Model.IsGrounded = false;

                AudioService.Instance.PlaySound(SoundType.Jump);
            }
        }
    }

    // Input System message (custom input action)
    void OnFire(InputValue value)
    {
        if (!Controller.Model.IsAlive) return;

        if (value.isPressed)
        {
            animator.SetBool(Controller.Model.ShootBoolName, true); // start shooting animation
        }
    }

    // called via animation event
    public void ShootArrow()
    {
        ArrowService.Instance.SpawnArrow(arrowSpawnPoint.position, transform.localScale);
        AudioService.Instance.PlaySound(SoundType.ArrowShooting);
    }

    // called via animation event
    public void ShootingAnimationEnded()
    {
        Controller.Model.ShootingAnimationEnd = true;
    }

    private void ProcessPlayerDeath()
    {
        if (BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.HazardLayer) ||
            BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.WaterLayer))
        {
            Controller.Model.IsAlive = false;
            onPlayerDeath?.Invoke();

            animator.SetTrigger(Controller.Model.DeadTriggerName);
            AudioService.Instance.PlaySound(SoundType.Hurt);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyView>())
        {
            animator.SetBool(Controller.Model.HurtBoolName, true);

            RigidBody.velocity = Controller.Model.PushVelocity; // Pushes the player on collision with enemy

            DecreaseLives();
            AudioService.Instance.PlaySound(SoundType.Hurt);
        }

        if (BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.PlatformLayer))
        {
            animator.SetBool(Controller.Model.JumpBoolName, false);
            Controller.Model.IsGrounded = true;
        }
    }

    public void DecreaseLives()
    {
        Controller.Model.Lives--;

        if (Controller.Model.Lives >= 0)
            onPlayerLivesChange?.Invoke(Controller.Model.Lives);

        if (Controller.Model.Lives <= 0)
            ProcessPlayerDeath();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool(Controller.Model.HurtBoolName, false);
    }

    public void PlayFootStepSound()
    {
        if (Controller.Model.IsGrounded)
            AudioService.Instance.PlaySound(SoundType.Footsteps);
    }
}

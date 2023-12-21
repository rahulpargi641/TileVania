using System;
using UnityEngine;

public class PlayerController
{
    public static event Action<int> onPlayerLivesChange;
    public static event Action onPlayerDeath;

    #region Model Related
    public float GravityScaleAtStart { get { return model.GravityScaleAtStart; } set { model.GravityScaleAtStart = value; } }
    public bool IsGrounded { get { return model.IsGrounded; } set { model.IsGrounded = value; } }
    public Vector2 PushVelocity => model.PushVelocity;

    public string RunBoolName => model.RunBoolName;
    public string JumpBoolName => model.JumpBoolName;
    public string ClimbBoolName => model.ClimbBoolName;
    public string ShootBoolName => model.ShootBoolName;
    public string HurtBoolName => model.HurtBoolName;
    public string DeadTriggerName => model.DeadTriggerName;
    #endregion

    public bool CanShoot => model.IsAlive;

    private readonly PlayerModel model;
    private readonly PlayerView view;

    public PlayerController(PlayerView view, PlayerSO playerSO)
    {
        this.view = view;
        model = new PlayerModel(playerSO);

        this.view.Controller = this;
    }

    public void ProcessPlayerMovement()
    {
        if (!model.IsAlive) return;

        ProcessRunning();
        ProcessClimbingLadder();
        ProcessShooting();
        ProcessBouncyMashroomJump();
    }

    private void ProcessRunning()
    {
        Vector2 playerVelocity = new Vector2(view.MoveInput.x * model.RunSpeed, view.RigidBody.velocity.y);
        view.RigidBody.velocity = playerVelocity;

        view.RunAnimation();
    }

    private void ProcessClimbingLadder()
    {
        if (view.BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.ClimbingLayer)) // not touching the ladder
        {
            StartClimbing();
            view.ClimbAnimation(view.RigidBody.velocity.y > Mathf.Epsilon);
        }
        else
        {
            StopClimbing();
            view.ClimbAnimation(false);
        }
    }

    private void StartClimbing()
    {
        ApplyClimbVelocity();
        view.RigidBody.gravityScale = 0f; // Disable Gravity
    }

    private void ApplyClimbVelocity()
    {
        Vector2 climbVelocity = new Vector2(view.RigidBody.velocity.x, view.MoveInput.y * model.ClimbSpeed);
        view.RigidBody.velocity = climbVelocity;
    }

    private void StopClimbing()
    {
        view.RigidBody.gravityScale = model.GravityScaleAtStart; // Restore gravity 
    }

    private void ProcessShooting()
    {
        if (view.ShootingAnimationEnd)
        {
            view.StopShootingAnimation(); // Shooting animation start playing via OnFire() action
            view.ShootingAnimationEnd = false;
        }
    }

    private void ProcessBouncyMashroomJump()
    {
        if (view.BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.BouncyMashorromLayer)) // Bouncy mashroom will add force to the player
            AudioService.Instance.PlaySound(SoundType.BouncyJump);
    }

    public void ProcessJump()
    {
        if (!model.IsAlive) return;

        if (IsGrounded)
        {
            Jump();
            view.JumpAnimation();
            AudioService.Instance.PlaySound(SoundType.Jump);
        }
    }

    private void Jump()
    {
        view.RigidBody.velocity = new Vector2(view.RigidBody.velocity.x, model.JumpSpeed);
        model.IsGrounded = false;
    }

    public void DecreaseLives()
    {
        model.Lives--;

        if (model.Lives >= 0)
            onPlayerLivesChange?.Invoke(model.Lives);

        if (model.Lives <= 0)
            ProcessDeath();
    }

    public void ShootArrow()
    {
        ArrowService.Instance.SpawnArrow(view.ArrowSpawnPoint.position, view.transform.localScale);
        AudioService.Instance.PlaySound(SoundType.ArrowShooting);
    }

    public void ProcessDeath()
    {
        model.IsAlive = false;
        onPlayerDeath?.Invoke();
        AudioService.Instance.PlaySound(SoundType.Hurt);
    }
}

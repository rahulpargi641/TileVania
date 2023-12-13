using UnityEngine;

public class PlayerController
{
    public PlayerModel Model { get; private set; }
    private readonly PlayerView view;

    public PlayerController(PlayerView view, PlayerSO playerSO)
    {
        this.view = view;
        Model = new PlayerModel(playerSO);

        this.view.Controller = this;
    }

    public void ProcessPlayerMovement()
    {
        ProcessRunning();
        ProcessClimbingLadder();
        ProcessShooting();
        ProcessBouncyMashroomJump();
    }

    private void ProcessRunning()
    {
        Vector2 playerVelocity = new Vector2(view.MoveInput.x * Model.RunSpeed, view.RigidBody.velocity.y);
        view.RigidBody.velocity = playerVelocity;

        view.PlayerRunAnimation();
    }

    private void ProcessClimbingLadder()
    {
        if (!view.BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.ClimbingLayer))
        {
            view.RigidBody.gravityScale = Model.GravityScaleAtStart;

            view.PlayerClimbAnimation(false);
            return;
        }

        Vector2 climbVelocity = new Vector2(view.RigidBody.velocity.x, view.MoveInput.y * Model.ClimbSpeed);
        view.RigidBody.velocity = climbVelocity;
        view.RigidBody.gravityScale = 0f;

        view.PlayerClimbAnimation(view.RigidBody.velocity.y > Mathf.Epsilon);
    }

    private void ProcessShooting()
    {
        if (Model.ShootingAnimationEnd)
        {
            view.StopShootingAnimation(); // Shooting animation start playing via OnFire() action
            Model.ShootingAnimationEnd = false;
        }
    }

    private void ProcessBouncyMashroomJump()
    {
        if (view.BodyCapsuleCollider.IsTouchingLayers(LayersManager.Instance.BouncyMashorromLayer))
            AudioService.Instance.PlaySound(SoundType.BouncyJump);
    }
}

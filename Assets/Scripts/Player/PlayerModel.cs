using UnityEngine;

public class PlayerModel
{
    public float RunSpeed { get; private set; }
    public float JumpSpeed { get; private set; }
    public float ClimbSpeed { get; private set; }
    public Vector2 pushVelocity { get; private set; }
    public int Lives { get; set; }
    public bool IsAlive { get;  set; } = true;
    public bool CanJump { get; set; } = true;
    public bool ShootingAnimationEnd { get; set; } = false;
    public float GravityScaleAtStart { get; set; }

    private PlayerSO playerSO;

    public PlayerModel(PlayerSO playerSO)
    {
        this.playerSO = playerSO;

        RunSpeed = playerSO.runSpeed;
        JumpSpeed = playerSO.jumpSpeed;
        ClimbSpeed = playerSO.climbSpeed;
        pushVelocity = playerSO.pushVelocity;

        Lives = playerSO.lives;
    }
}

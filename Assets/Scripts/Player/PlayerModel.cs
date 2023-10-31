using UnityEngine;

public class PlayerModel
{
    public float RunSpeed { get; private set; }
    public float JumpSpeed { get; private set; }
    public float ClimbSpeed { get; private set; }
    public Vector2 pushVelocity { get; private set; }
    public int Lives { get; set; }
    public bool IsAlive { get;  set; } = true;
    public bool IsGrounded { get; set; } = true;
    public bool ShootingAnimationEnd { get; set; } = false;
    public float GravityScaleAtStart { get; set; }

    public string idleBoolName { get; private set; }
    public string RunBoolName { get; private set; }
    public string JumpBoolName { get; private set; }
    public string ClimbBoolName { get; private set; }
    public string ShootBoolName { get; private set; }
    public string HurtBoolName { get; private set; }
    public string DeadTriggerName { get; private set; }

    private PlayerSO playerSO;

    public PlayerModel(PlayerSO playerSO)
    {
        this.playerSO = playerSO;

        RunSpeed = playerSO.runSpeed;
        JumpSpeed = playerSO.jumpSpeed;
        ClimbSpeed = playerSO.climbSpeed;
        pushVelocity = playerSO.pushVelocity;

        Lives = playerSO.lives;

        idleBoolName = playerSO.idleBoolName;
        RunBoolName = playerSO.runBoolName;
        JumpBoolName = playerSO.jumpBoolName;
        ClimbBoolName = playerSO.climbBoolName;
        ShootBoolName = playerSO.shootBoolName;
        HurtBoolName = playerSO.hurtBoolName;
        DeadTriggerName = playerSO.deadTriggerName;
    }
}

using UnityEngine;

public class PlayerModel
{
    public float RunSpeed { get; private set; }
    public float JumpSpeed { get; private set; }
    public float ClimbSpeed { get; private set; }
    public Vector2 DeathKick { get; private set; }
    public bool IsAlive { get; set; } = true;
    public bool CanJump { get; set; } = true;
    public bool ShootingAnimationEnd { get; set; } = false;
    public float GravityScaleAtStart { get; set; }

    public PlayerModel()
    {
        RunSpeed = 8f;
        JumpSpeed = 9f;
        ClimbSpeed = 5f;
        DeathKick = new Vector2(5f, 5f);
    }
}

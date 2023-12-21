using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Player Info")]
    public new string name;
    public int lives = 3;

    public PlayerView playerView;

    [Header("Movement")]
    public float runSpeed = 6.5f;
    public float jumpSpeed = 9f;
    public float climbSpeed = 7f;
    public Vector2 pushVelocity = new Vector2(5f, 5f);

    [Header("Animator Parameters Names")]
    public string idleBoolName = "Idling";
    public string runBoolName = "Running";
    public string jumpBoolName = "Jumping";
    public string climbBoolName = "Climbing";
    public string shootBoolName = "Shooting";
    public string hurtBoolName = "Hurt";
    public string deadTriggerName = "Die";
}




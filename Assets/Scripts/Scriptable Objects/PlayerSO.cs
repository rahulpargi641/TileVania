using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Player Info")]
    public new string name;
    public PlayerPresenter playerView;
    public int lives = 3;

    [Header("Movement")]
    public float runSpeed = 6.5f;
    public float jumpSpeed = 9f;
    public float climbSpeed = 7f;
    public Vector2 pushVelocity = new Vector2(5f, 5f);


    [Header("Animation Names")]
    public string idleAnimName = "Idle";
    public string runAnimName = "Run";
    public string jumpAnimName = "Jump";
    public string climbAnimName = "Climb";
    public string shootAnimName = "Shoot";
    public string deadAnimName = "Dead";

    //[Header("Properties")]
    //public ItemType itemType; // Enum to specify the item type (e.g., Consumable, Weapon, Armor, etc.).
}




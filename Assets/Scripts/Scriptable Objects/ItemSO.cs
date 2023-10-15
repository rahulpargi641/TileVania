using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public string name;
    public int itemID;
    public ItemType itemType;
    public int value;
    public int speed;
}

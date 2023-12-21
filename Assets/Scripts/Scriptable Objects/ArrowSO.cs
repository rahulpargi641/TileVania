using UnityEngine;

[CreateAssetMenu(fileName = "Arrow", menuName = "ScriptableObjects/Items/Arrow")]

public class ArrowSO : ScriptableObject
{
    public float speed;
    public ArrowView arrowPrefab;                    
}
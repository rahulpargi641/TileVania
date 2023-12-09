using UnityEngine;

[CreateAssetMenu(fileName = "Arrow", menuName = "ScriptableObjects/Items/Arrow")]

public class ArrowSO : ScriptableObject
{
    public float speed;
    public ArrowView arrowPrefab; // For scalability purposes, for adding different types of arrows in future and to be
                                     // Consistent throught the code base, since service will spawn the arrow from the arrow SO.
                                     // that's why storing prefabs in the Scriptable objects.
                                    
}
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "ScriptableObjects/Items/Coin")]
public class CoinSO : ScriptableObject
{
    public int pointsGain = 10;
    public CoinView coinView;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "ScriptableObjects/Items/Coin")]
public class CoinSO : ItemSO
{
    public CoinPresenter coinPresenter;
    public int coinPointsGain = 10;
}

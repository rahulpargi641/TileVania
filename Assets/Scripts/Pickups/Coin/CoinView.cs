using System;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    public static event Action<int> onCoinPickup;
    public static event Action<CoinView> onCoinPickedUp;

    private CoinModel model;

    public void InitializeModel(CoinSO coinSO)
    {
        model = new CoinModel(coinSO);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>())
        {
            onCoinPickup?.Invoke(model.pointsGain);
            AudioService.Instance.PlaySound(SoundType.CoinPickup);
            onCoinPickedUp?.Invoke(this); // returns coin to the pool
        }
    }

    public void EnableCoin()
    {
        gameObject.SetActive(true);
    }

    public void DisableCoin()
    {
        gameObject.SetActive(false);
    }

    public void SetTransform(Vector2 spawnPointPos)
    {
        transform.position = spawnPointPos;
    }
}

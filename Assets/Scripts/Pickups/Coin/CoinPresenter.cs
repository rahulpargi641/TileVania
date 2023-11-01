using System;
using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    public static event Action<int> onCoinPickup;

    private CoinModel model;

    public void InitializeModel(CoinModel model)
    {
        this.model = model;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            onCoinPickup?.Invoke(model.coinPointsGain);
            AudioService.Instance.PlaySound(SoundType.CoinPickup);
            CoinService.Instance.ReturnCoinToPool(this);
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

    internal void SetTransform(Vector2 spawnPointPos)
    {
        transform.position = spawnPointPos;
    }
}

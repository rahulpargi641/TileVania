using System;
using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    // Implement item Scriptable object

    public static event Action<int> onCoinPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            onCoinPickup?.Invoke(10);
            AudioService.Instance.PlaySound(SoundType.CoinPickup);
            gameObject.SetActive(false);
        }
    }
}

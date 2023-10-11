using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            AudioService.Instance.PlaySound(SoundType.CoinPickup);
            gameObject.SetActive(false);
        }
    }
}

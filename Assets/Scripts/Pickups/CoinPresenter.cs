using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPresenter>())
        {
            gameObject.SetActive(false);
        }
    }
}

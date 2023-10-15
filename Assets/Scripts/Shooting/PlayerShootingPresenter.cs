using UnityEngine;

public class PlayerShootingPresenter : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform spawnPoint;

    public void ShootArrow()  // Shoot Arrow
    {
        ArrowService.Instance.SpawnArrow(spawnPoint.position, transform.localScale);
    }
}

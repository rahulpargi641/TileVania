using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoSingletonGeneric<ShootingController>
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform spawnPoint;

    private List<GameObject> arrowPool;
    private int poolSize = 10;
    private float spawnRate = 1.0f;
    private float nextSpawnTime;


    // Start is called before the first frame update
    private void Start()
    {
        arrowPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    public void SpawnArrow()
    {
        for (int i = 0; i < arrowPool.Count; i++)
        {
            if (!arrowPool[i].activeInHierarchy)
            {
                arrowPool[i].transform.position = spawnPoint.position;

                Vector2 localScale = arrowPool[i].transform.localScale;
                arrowPool[i].transform.localScale = new Vector2(localScale.x, Mathf.Sign(transform.localScale.x));

                arrowPool[i].transform.rotation = arrowPrefab.transform.rotation;
                arrowPool[i].SetActive(true);
                return;
            }
        }
    }
}

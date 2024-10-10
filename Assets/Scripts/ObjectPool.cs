using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.1f, 2f)] float spawnTimeDelay = 1f;
    [SerializeField][Range(0, 10)] int poolSize = 5;

    private GameObject[] pool;
    // Start is called before the first frame update
    void Start()
    {
        PopulatePool();
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }
    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        if (enemyPrefab)
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(spawnTimeDelay);

            }

        }
    }
}

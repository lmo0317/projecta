using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float RespawnTime = 3;

    public GameObject[] spawnPositions;


    //private
    private float _spawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTime += Time.deltaTime;

        if (_spawnTime > RespawnTime)
        {
            int idx = Random.Range(0, spawnPositions.Length);
            GameObject spawnPosition = spawnPositions[idx];

            Instantiate(enemyPrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);

            _spawnTime = 0.0f;
        }
    }
}

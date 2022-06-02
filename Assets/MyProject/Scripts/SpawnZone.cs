using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/enemy1");
        var enemy = Instantiate(prefab, transform.position, transform.rotation);

        EnemyManager.Instance.AddEnemy(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

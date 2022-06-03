using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    enum SpawnType
    {
        Start,
        Touch
    }

    private GameObject _prefab;
    private SphereCollider _sphereCollider;

    [SerializeField]
    private int _maxCount = 0;

    [SerializeField]
    private SpawnType _spawnType = SpawnType.Start;


    private int _spawnedCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        _prefab = Resources.Load<GameObject>("Prefabs/enemy1");
        _sphereCollider = transform.GetComponent<SphereCollider>();

        if (_spawnType == SpawnType.Start)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, _sphereCollider.radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_spawnType == SpawnType.Touch)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_prefab, transform.position, transform.rotation);
        var control = enemy.GetComponent<MonsterCtrl>();
        EnemyManager.Instance.AddEnemy(control);
        Destroy(gameObject);
    }
}

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
    private int _range = 0;

    [SerializeField]
    private SpawnType _spawnType = SpawnType.Start;

    private int _spawnedCount = 0;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (_spawnType == SpawnType.Touch)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        for(int i =0; i < _maxCount; i++)
        {
            SpawnEnemyImpl();
        }

        Destroy(gameObject);
    }

    private void SpawnEnemyImpl()
    {
        var offset = new Vector3(UnityEngine.Random.RandomRange(0, _range), 0, UnityEngine.Random.RandomRange(0, _range));
        var enemy = Instantiate(_prefab, transform.position + offset, transform.rotation);
        var control = enemy.GetComponent<MonsterCtrl>();
        EnemyManager.Instance.AddEnemy(control);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private static readonly Lazy<EnemyManager> instance = new Lazy<EnemyManager>(() => new EnemyManager());

    private List<GameObject> _enemys = new List<GameObject>();

    public static EnemyManager Instance
    {
        get { return instance.Value; }
    }

    private EnemyManager()
    {

    }

    public void AddEnemy(GameObject enemy)
    {
        _enemys.Add(enemy);
        Debug.Log($"AddEnemy [{_enemys.Count}]");
    }

    public void KillEnemy(GameObject enemy)
    {
        foreach (var e in _enemys)
        {
            if(e == enemy)
            {
                _enemys.Remove(e);
                break;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private static readonly Lazy<EnemyManager> instance = new Lazy<EnemyManager>(() => new EnemyManager());

    private List<Monster> _enemys = new List<Monster>();

    public static EnemyManager Instance
    {
        get { return instance.Value; }
    }

    private EnemyManager()
    {

    }

    public void AddEnemy(Monster enemy)
    {
        _enemys.Add(enemy);
    }

    public void KillEnemy(Monster enemy)
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

    public int GetRemainEnemeyCount()
    {
        return _enemys.Count;
    }
}

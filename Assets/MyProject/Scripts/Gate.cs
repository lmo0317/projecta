using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawnZones;

    private bool _isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(IsEverySpawnZoneClear() == true && _isOpen == false)
        {
            _isOpen = true;
            Destroy(gameObject);
        }
    }

    public bool IsEverySpawnZoneClear()
    {
        foreach (var spawnZone in _spawnZones)
        {
            if (spawnZone != null)
            {
                return false;
            }
        }

        return true;
    }
}

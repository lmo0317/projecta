using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private GameObject _door;

    private bool _isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( EnemyManager.Instance.GetRemainEnemeyCount() <= 0 )
        {
            _door.SetActive(false);
            _isOpen = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(_isOpen)
        {
            GameManager.Instance.NextStage();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isOpen)
        {
            GameManager.Instance.NextStage();
        }
    }
}

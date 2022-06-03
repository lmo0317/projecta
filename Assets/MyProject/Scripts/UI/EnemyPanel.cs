using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    [SerializeField]
    private Text _remainCount;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _remainCount.text = $"REMAIN = [{EnemyManager.Instance.GetRemainEnemeyCount()}]";
    }
}

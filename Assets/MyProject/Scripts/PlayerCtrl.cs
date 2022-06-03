using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update

    private readonly float initHp = 100.0f;

    public float currHp;

    void Start()
    {
        currHp = initHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currHp >= 0.0f && other.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
        }
    }
}

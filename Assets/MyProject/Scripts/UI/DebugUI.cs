using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() // deprecated, use ordinary .UI now available in Unity
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Debug!"))
        {
            //EventManager.animationEvent.OnNext(10);
        }
    }
}

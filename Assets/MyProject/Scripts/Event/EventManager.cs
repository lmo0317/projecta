using UnityEngine;
using System.Collections;
using UniRx;

public class EventManager : MonoBehaviour
{
    public static UniRx.Subject<int> animationEvent = new ();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

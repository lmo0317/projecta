using UnityEngine;
using System.Collections;
using UniRx;
using System;

public struct AnimationEventData
{
    GameObject parent;
    string value;
    Vector3 pos;
}

public class EventDispatcher
{
    private static readonly Lazy<EventDispatcher> instance = new Lazy<EventDispatcher>(() => new EventDispatcher());

    public static EventDispatcher Instance
    {
        get { return instance.Value; }
    }

    public UniRx.Subject<AnimationEventData> AnimationEvent { get; set; } = new();


    public void DispatchAnimationEvent(AnimationEventData value)
    {
        AnimationEvent.OnNext(value);
    }
}

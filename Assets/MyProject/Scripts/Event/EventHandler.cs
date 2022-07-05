using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using System.Collections;

public class EventHandler : MonoBehaviour
{
    public void Start()
    {

        //EventDispatcher.Instance.AnimationEvent.Subscribe(OnAnimationEvent);
    }

    private void OnAnimationEvent(string value)
    {
        Debug.Log($"EventHandler::OnAnimationEvent - Value : {value}");
    }
}
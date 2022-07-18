using System;
using UnityEditor;
using UnityEngine;


public class UIManager
{
    private static readonly Lazy<UIManager> instance = new Lazy<UIManager>(() => new UIManager());

    public static UIManager Instance
    {
        get { return instance.Value; }
    }

    private StatusPanel _statusPanel;

    public void SetStatusPanel(StatusPanel statusPanel)
    {
        _statusPanel = statusPanel;
    }

    public void SetHP(float ratio)
    {
        if (_statusPanel == null)
            return;

        _statusPanel.HP.value = ratio;
    }
}
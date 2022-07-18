using System.Collections;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    public UnityEngine.UI.Slider HP;

    // Use this for initialization
    void Start()
    {
        UIManager.Instance.SetStatusPanel(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
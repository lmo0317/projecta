using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatusBarComponent : MonoBehaviour
{
    private Monster _monster;
    private float _hitRatio;

    [Header("PopUpText Settings")]
    [Tooltip("PopUpText prefab")]
    public GameObject PopUpPrefab;

    [Tooltip("PopUpText Color")]
    public Color PopUpTextColor = Color.red;

    [Tooltip("PopUpText fade time")]
    public float FadeTime = 0.5f;

    public Image CurrentHitPointImage;

    private void Start()
    {
        _monster = GetComponent<Monster>();
    }


    public void UpdatePointsBars()
    {
        _hitRatio = _monster.CurrentHP / _monster.MaxHP;
        CurrentHitPointImage.rectTransform.localScale = new Vector3(_hitRatio, 1, 1);
    }

    public void InstancePopUp(string popUpText)
    {
        var poPupText =
            Instantiate(PopUpPrefab, transform.position + UnityEngine.Random.insideUnitSphere * 0.4f,
                transform.rotation);
        Destroy(poPupText, FadeTime);
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().text = popUpText;
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().color = PopUpTextColor;
    }
}
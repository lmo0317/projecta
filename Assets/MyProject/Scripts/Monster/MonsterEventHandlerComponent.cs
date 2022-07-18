using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class MonsterEventHandlerComponent : MonoBehaviour
{
    Monster _monster;

    public void Start()
    {
        _monster = GetComponent<Monster>();
    }

    #region animation event handler
    public void AnimationEventHandler(string param1)
    {
        if (param1 == AnimationConsts.ANIMATION_EVENT_ATTACK)
        {
            //var skillEffect = Resources.Load<GameObject>("Prefabs/skill/SwordHitBlue");
            StartCoroutine(SkillManager.Instance.GenerateSkillEffect(null, _monster));
        }
    }
    #endregion

}

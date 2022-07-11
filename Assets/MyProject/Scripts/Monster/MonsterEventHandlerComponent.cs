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
            StartCoroutine(GenerateBoxCollider());
        }
    }

    private IEnumerator GenerateBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = _monster.AttackDummy.transform.localPosition;
        boxCollider.size = new Vector3(4, 4, 4);
        boxCollider.isTrigger = true;
        //boxCollider.tag = TagUtil.TAG_MONSTER_ATTACK_COLLIDER;

        yield return new WaitForSeconds(0.2f);

        Destroy(boxCollider);
        yield return null;
    }
    #endregion

}

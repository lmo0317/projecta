using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float InitHp = 100.0f;

    public float currHp;

    public GameObject SkillEffect;
    public GameObject ChangeEffect;

    void Start()
    {
        currHp = InitHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currHp >= 0.0f && other.CompareTag(TagUtil.TAG_MONSTER_ATTACK_COLLIDER))
        {
            currHp -= 10.0f;
            if(currHp < 0)
            {
                SetStateDie();
            }
        }
    }

    private void SetStateDie()
    {

    }

    public void DoSkill()
    {
        StartCoroutine(GenerateSkillEffect());
    }

    public void DoChange()
    {

    }

    IEnumerator GenerateSkillEffect()
    {
        var skillEffect = Instantiate(SkillEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(5);

        Destroy(skillEffect);

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DamageEffectPrefab;
    public float InitHp = 100.0f;
    public float currHp;

    void Start()
    {
        currHp = InitHp;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DoSkill(0);
        }
    }

    #region collision
    private void OnTriggerEnter(Collider collider)
    {
        var skillContainer = collider.GetComponent<SkillContainer>();
        if (skillContainer && skillContainer.IsMonsterTheOwner() && IsDie() == false)
        {
            OnDamage();
        }
    }
    #endregion

    private void OnDamage()
    {
        currHp -= 10.0f;
        DamageEffect();

        if (currHp < 0)
        {
            SetStateDie();
        }
    }

    private void DamageEffect()
    {
        Instantiate(DamageEffectPrefab, transform.position, transform.rotation);
    }

    private bool IsDie()
    {
        return currHp <= 0.0f;
    }

    private void SetStateDie() 
    {

    }

    public void DoSkill(int id)
    {
        var skillEffect = Resources.Load<GameObject>("Prefabs/skill/NovaSkillEffect");
        StartCoroutine(SkillManager.Instance.GenerateSkillEffect(skillEffect, this));
    }

    public void DoChange()
    {

    }
}

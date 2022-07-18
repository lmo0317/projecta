using System;
using System.Collections;
using UnityEngine;


public class SkillManager : MonoSingleton<SkillManager>
{
    private GameObject _skillContainerPrefab;


    private void Awake()
    {
        _skillContainerPrefab = Resources.Load<GameObject>("Prefabs/skill/SkillContainer");
    }

    public void DoSkill(int id, MonoBehaviour owner)
    {
        var skillEffect = Resources.Load<GameObject>("Prefabs/skill/NovaSkillEffect");
        StartCoroutine(GenerateSkillEffect(skillEffect, owner));
    }
    public IEnumerator GenerateSkillEffect(GameObject skillEffectPrefab, MonoBehaviour owner)
    {
        var position = owner.transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        var scale = new Vector3(2.0f, 2.0f, 2.0f);

        //skill container 생성
        var skillContainerInstance = Instantiate(_skillContainerPrefab, position, rotation);
        var skillContainer = skillContainerInstance.GetComponent<SkillContainer>();
        skillContainer.Owner = owner;
        skillContainer.Damage = 50;

        //Effect 생성
        if (skillEffectPrefab != null)
        {
            var skillEffect = Instantiate(skillEffectPrefab, skillContainer.transform);
            skillEffect.transform.localScale = scale;
            skillEffect.transform.rotation = rotation;
        }

        //Collider 생성
        BoxCollider boxCollider = skillContainerInstance.AddComponent<BoxCollider>();
        boxCollider.center = Vector3.zero;
        boxCollider.size = new Vector3(10, 10, 10);
        boxCollider.isTrigger = true;

        yield return new WaitForSeconds(2.0f);
        Destroy(boxCollider);

        yield return new WaitForSeconds(5);
        Destroy(skillContainer);

        yield return null;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerSkillComponent : MonoBehaviour
{
    public GameObject SkillEffect;
    public GameObject ChangeEffect;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public void DoSkill(int id)
    {
        //id에 해당 되는 스킬 정보 얻어 와서 스킬 발사
        StartCoroutine(GenerateSkillEffect());
    }

    public void DoChange()
    {

    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            DoSkill(0);
        }
    }

    IEnumerator GenerateSkillEffect()
    {
        if (_player == null)
            yield return null;

        var position = _player.transform.position;
        Quaternion rotation = Quaternion.Euler(0 , 0, 0);
        var scale = new Vector3(2.0f, 2.0f, 2.0f);

        //Effect 생성
        var skillEffect = Instantiate(SkillEffect, _player.transform.position, rotation);
        skillEffect.transform.localScale = scale;

        //Collider 생성
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = Vector3.zero;
        boxCollider.size = new Vector3(20 ,20,20);
        boxCollider.isTrigger = true;
        boxCollider.tag = TagUtil.TAG_GENERATED_COLLIDER;
        yield return new WaitForSeconds(2);
        Destroy(boxCollider);

        yield return new WaitForSeconds(5);
        Destroy(skillEffect);

        yield return null;
    }
}
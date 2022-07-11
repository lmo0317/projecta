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
    public GameObject SkillContainer;

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

        //skill container 생성
        var skillContainer = Instantiate(SkillContainer, _player.transform.position, rotation);

        //Effect 생성
        var skillEffect = Instantiate(SkillEffect, skillContainer.transform);
        skillEffect.transform.localScale = scale;
        skillEffect.transform.rotation = rotation;

        //Collider 생성
        BoxCollider boxCollider = skillContainer.AddComponent<BoxCollider>();
        boxCollider.center = Vector3.zero;
        boxCollider.size = new Vector3(10,10,10);
        boxCollider.isTrigger = true;

        yield return new WaitForSeconds(2);
        Destroy(boxCollider);

        yield return new WaitForSeconds(5);
        Destroy(skillContainer);

        yield return null;
    }
}
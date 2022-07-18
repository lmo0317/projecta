using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
using UnityEngine.AI;
using TopDownShooter;

public partial class Monster : MonoBehaviour
{
    public float MaxHP = 100;
    public float CurrentHP = 0; 
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    public bool isDie = false;
    public GameObject AttackDummy;
    public MonsterEnum.State state = MonsterEnum.State.IDLE;

    //private int hp = 100;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    private MonsterStatusBarComponent _monsterStatusBarComponent;

    void OnEnable()
    {
        //PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
    }

    void OnDisable()
    {
        //PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    void Start()
    {
        CurrentHP = MaxHP;
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag(TagUtil.TAG_PLAYER).GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _monsterStatusBarComponent = GetComponent<MonsterStatusBarComponent>();
        
        StartCoroutine(AIProcess());
    }

    IEnumerator AIProcess()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (state == MonsterEnum.State.DIE) 
                yield break;

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                SetStateAttack();
            }
            else if (distance <= traceDist)
            {
                SetStateTrace();
            }
            else
            {
                SetStateIdle();
            }
        }
    }

    private void SetStateAttack()
    {
        state = MonsterEnum.State.ATTACK;
        anim.SetBool(hashAttack, true);
    }

    private void SetStateTrace()
    {
        state = MonsterEnum.State.TRACE;
        agent.SetDestination(playerTr.position);
        agent.isStopped = false;
        anim.SetBool(hashTrace, true);
        anim.SetBool(hashAttack, false);
    }

    private void SetStateIdle()
    {
        state = MonsterEnum.State.IDLE;
        agent.isStopped = true;
        anim.SetBool(hashTrace, false);
    }

    private void SetStateDie()
    {
        isDie = true;
        agent.isStopped = true;
        anim.SetTrigger(hashDie);
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.2f));
        anim.SetTrigger(hashPlayerDie);
    }

    private void Dead()
    {
        Destroy(gameObject, 0);
        EnemyManager.Instance.KillEnemy(this);
    }

    #region collision handler
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(TagUtil.TAG_BULLET))
        {
            Destroy(collision.gameObject);
            anim.SetTrigger(hashHit);

            Vector3 pos = collision.GetContact(0).point;
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);

            if (collision.transform.GetComponent<Damage>())
            {
                ApplyDamage(collision.transform.GetComponent<Damage>().DamagePower);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        var skillContainer = collider.GetComponent<SkillContainer>();
        if(skillContainer && skillContainer.IsPlayerTheOwner())
        {
            //skill container에서 skill정보 얻어 와서 공격 추가
            ApplyDamage(skillContainer.Damage);
        }
    }

    public void ApplyDamage(float amount)
    {
        CurrentHP -= amount;

        if (_monsterStatusBarComponent)
        {
            _monsterStatusBarComponent.UpdatePointsBars();
            _monsterStatusBarComponent.InstancePopUp(amount.ToString(CultureInfo.InvariantCulture));
        }

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Dead();
        }
    }

    #endregion
}

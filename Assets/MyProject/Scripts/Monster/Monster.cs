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
    #region damage popup
    [Header("PopUpText Settings")]
    [Tooltip("PopUpText prefab")]
    public GameObject PopUpPrefab;

    [Tooltip("PopUpText Color")]
    public Color PopUpTextColor = Color.red;

    [Tooltip("PopUpText fade time")]
    public float FadeTime = 0.5f;

    public Image CurrentHitPointImage;
    #endregion

    public float MaxHP = 100;
    public float CurrentHP = 0;
    private float _hitRatio;

    public GameObject AttackDummy;

    public MonsterEnum.State state = MonsterEnum.State.IDLE;
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    public bool isDie = false;

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
        StartCoroutine(AIProcess());
    }

    private void UpdatePointsBars()
    {
        _hitRatio = CurrentHP / MaxHP;
        CurrentHitPointImage.rectTransform.localScale = new Vector3(_hitRatio, 1, 1);
    }

    private void InstancePopUp(string popUpText)
    {
        var poPupText =
            Instantiate(PopUpPrefab, transform.position + Random.insideUnitSphere * 0.4f,
                transform.rotation);
        Destroy(poPupText, FadeTime);
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().text = popUpText;
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().color = PopUpTextColor;
    }

    IEnumerator AIProcess()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (state == MonsterEnum.State.DIE) yield break;

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
        boxCollider.center = AttackDummy.transform.localPosition;
        boxCollider.size = new Vector3(4, 4, 4);
        boxCollider.isTrigger = true;
        boxCollider.tag = TagUtil.TAG_MONSTER_ATTACK_COLLIDER;

        yield return new WaitForSeconds(0.2f);

        Destroy(boxCollider);
        yield return null;
    }
    #endregion

    #region collision handler
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag(TagUtil.TAG_BULLET))
        {
            Destroy(coll.gameObject);
            anim.SetTrigger(hashHit);

            Vector3 pos = coll.GetContact(0).point;
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            CurrentHP -= 10;

            if (coll.transform.GetComponent<Damage>())
            {
                ApplyDamage(coll.transform.GetComponent<Damage>().DamagePower);
            }
        }
    }

    public void ApplyDamage(float amount)
    {
        CurrentHP -= amount;
        UpdatePointsBars();

        if (PopUpPrefab)
        {
            InstancePopUp(amount.ToString(CultureInfo.InvariantCulture));
        }

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Dead();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    #endregion
}

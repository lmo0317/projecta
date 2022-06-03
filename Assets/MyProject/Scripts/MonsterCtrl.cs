using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

using UnityEngine.AI;
using TopDownShooter;

public class MonsterCtrl : MonoBehaviour
{
    [Header("PopUpText Settings")]
    [Tooltip("PopUpText prefab")]
    public GameObject PopUpPrefab;

    [Tooltip("PopUpText Color")] 
    public Color PopUpTextColor = Color.red;
    
    [Tooltip("PopUpText fade time")] 
    public float FadeTime = 0.5f;

    public float MaxHP = 100;
    public float CurrentHP = 0;
    
    public Image CurrentHitPointImage;
    //public FlashOnDamage FlashOnDamage;

    private float _hitRatio;

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    // ������ ���� ����
    public State state = State.IDLE;
    // ���� �����Ÿ�
    public float traceDist = 10.0f;
    // ���� �����Ÿ�
    public float attackDist = 2.0f;
    // ������ ��� ����
    public bool isDie = false;

    // ������Ʈ�� ĳ�ø� ó���� ����
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    // Animator �Ķ������ �ؽð� ����
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    // ���� ȿ�� ������
    //private GameObject bloodEffect;

    // ���� ���� ����
    //private int hp = 100;

    void OnEnable()
    {
        // �̺�Ʈ �߻� �� ������ �Լ� ����
        //PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
    }

    // ��ũ��Ʈ�� ��Ȱ��ȭ�� ������ ȣ��Ǵ� �Լ�
    void OnDisable()
    {
        // ������ ����� �Լ� ����
        //PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    public void ApplyDamage(float amount)
    {
        CurrentHP -= amount;
        UpdatePointsBars();

        //if flash component exit start damage flash
        //if (FlashOnDamage)
        //{
        //    FlashOnDamage.StartDamageFlash();
        //}

        //if pop up component exist instantiate pop up text
        if (PopUpPrefab)
        {
            InstancePopUp(amount.ToString(CultureInfo.InvariantCulture));
        }

        //if current hit point is <= 0 kill the player
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Dead();
        }
    }

    private void UpdatePointsBars()
    {
        _hitRatio = CurrentHP / MaxHP;
        CurrentHitPointImage.rectTransform.localScale = new Vector3(_hitRatio, 1, 1);
    }

    //instance the popUp text
    //You can change this to text mesh pro or another GUI solutions.
    private void InstancePopUp(string popUpText)
    {
        var poPupText =
            Instantiate(PopUpPrefab, transform.position + Random.insideUnitSphere * 0.4f,
                transform.rotation);
        Destroy(poPupText, FadeTime);
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().text = popUpText;
        poPupText.transform.GetChild(0).GetComponent<TextMesh>().color = PopUpTextColor;
    }

    private void Dead()
    {
        //you can add dead animation on this place
        Destroy(gameObject, 0);
        EnemyManager.Instance.KillEnemy(this);
    }

    void Start()
    {
        CurrentHP = MaxHP;

        // ������ Transform �Ҵ�
        monsterTr = GetComponent<Transform>();

        // ���� ����� Player�� Transform �Ҵ�
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // NavMeshAgent ������Ʈ �Ҵ�
        agent = GetComponent<NavMeshAgent>();

        // Animator ������Ʈ �Ҵ�
        anim = GetComponent<Animator>();

        // BloodSprayEffect ������ �ε�
        //bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

        // ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckMonsterState());
        // ���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }

    // ������ �������� ������ �ൿ ���¸� üũ
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            // 0.3�� ���� ����(���)�ϴ� ���� ������� �޽��� ������ �纸
            yield return new WaitForSeconds(0.3f);

            // ������ ���°� DIE�� �� �ڷ�ƾ�� ����
            if (state == State.DIE) yield break;

            // ���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            // ���� �����Ÿ� ������ ���Դ��� Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            // ���� �����Ÿ� ������ ���Դ��� Ȯ��
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    // ������ ���¿� ���� ������ ������ ����
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                // IDLE ����
                case State.IDLE:
                    // ���� ����
                    agent.isStopped = true;

                    // Animator�� IsTrace ������ false�� ����
                    anim.SetBool(hashTrace, false);
                    break;

                // ���� ����
                case State.TRACE:
                    // ���� ����� ��ǥ�� �̵� ����
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    // Animator�� IsTrace ������ true�� ����
                    anim.SetBool(hashTrace, true);

                    // Animator�� IsAttack ������ false�� ����
                    anim.SetBool(hashAttack, false);
                    break;

                // ���� ����
                case State.ATTACK:
                    // Animator�� IsAttack ������ true�� ����
                    anim.SetBool(hashAttack, true);
                    break;

                // ���
                case State.DIE:
                    isDie = true;

                    // ���� ����
                    agent.isStopped = true;

                    // ��� �ִϸ��̼� ����
                    anim.SetTrigger(hashDie);

                    // ������ Collider ������Ʈ ��Ȱ��ȭ
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            // �浹�� �Ѿ��� ����
            Destroy(coll.gameObject);
            // �ǰ� ���׼� �ִϸ��̼� ����
            anim.SetTrigger(hashHit);

            // �Ѿ��� �浹 ����
            Vector3 pos = coll.GetContact(0).point;
            // �Ѿ��� �浹 ������ ���� ����
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            // ���� ȿ���� �����ϴ� �Լ� ȣ��
            //ShowBloodEffect(pos, rot);

            // ������ hp ����
            CurrentHP -= 10;

            if (CurrentHP <= 0)
            {
                state = State.DIE;
            }

            if (coll.transform.GetComponent<Damage>())
            {
                ApplyDamage(coll.transform.GetComponent<Damage>().DamagePower);
            }
        }
    }

    void OnDrawGizmos()
    {
        // ���� �����Ÿ� ǥ��
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        // ���� �����Ÿ� ǥ��
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    void OnPlayerDie()
    {
        // ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ��� ������Ŵ
        StopAllCoroutines();

        // ������ �����ϰ� �ִϸ��̼��� ����
        agent.isStopped = true;
        anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.2f)); // ��ũ��Ʈ 6-10 ���� �߰��ؾ� �ϴ� �ڵ�
        anim.SetTrigger(hashPlayerDie);
    }
}

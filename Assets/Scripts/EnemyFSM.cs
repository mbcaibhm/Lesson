using Nethereum.Contracts.Standards.ERC20.ContractDefinition;
using System;
using System.Collections;
using UnityEngine;
using UnityPipeline.Microsoft.CodeAnalysis.Operations;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }

    EnemyState state;

    public float findRange = 15f;
    public float moveRange = 30f;
    public float attackRange = 2f;
    Animator anim;
    Vector3 startPoint;
    Transform player;
    CharacterController cc;

    int hp = 100;
    int att = 5;
    float speed = 5f;

    float attTime = 2f;
    float timer = 0f;
    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = EnemyState.Idle;
        startPoint = transform.position;
        player = GameObject.Find("Player").transform;  //Find는 무거우니까 Update 사용에 주의하자
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return ();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) < findRange)
        {
            state = EnemyState.Move;
            print("상태전환: Idel -> Move");
            anim.SetTrigger("Move");
        }
    }

    private void Move()
    {
        if(Vector3.Distance(transform.position, startPoint) > moveRange)
        {
            state = EnemyState.Return;
            print("상태전환: move -> Return");
        }
        else if(Vector3.Distance(transform.position, player.position) > attackRange)
        {
            //Vector3 dir = (player.position - transform.position).normalized;
            Vector3 dir = (player.position - transform.position); //타겟팅 대상에서 자신의 포지션을 빼줌 = 타겟팅할 대상의 방향
            dir.Normalize();

            //transform.forward = dir;
            //transform.LookAt(player);
            //transform.forward = Vector3.Lerp(transform.forward, dir, 10 * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);


            //cc.Move(dir * speed * Time.deltaTime);
            cc.SimpleMove(dir * speed);

        }
        else
        {
            state = EnemyState.Attack;
            print("상태전환: move ->  Attack");
        }
    }

    private void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            timer += Time.deltaTime;
            if(timer > attTime)
            {
                print("공격");

                timer = 0f;
            }
        }
        else
        {
            state = EnemyState.Move;
            print("상태전환: Attack -> Move");
        }
    }

    private void Return()
    {
        if(Vector3.Distance(transform.position, startPoint) > 0.1f)
        {
            Vector3 dir = (startPoint - transform.position).normalized;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
            cc.SimpleMove(dir * speed);
        }
        else
        {
            transform.position = startPoint;
            state = EnemyState.Idle;
            print("상태전환: Return -> Idle");
        }
    }
    
    public void HitDamage(int value)
    {
        if (state == EnemyState.Damaged || state == EnemyState.Die) return;

        hp -= value;

        if(hp > 0)
        {
            state = EnemyState.Damaged;
            print("상태전환: AnyState -> Damaged");
            print("HP: " + hp);

            Damaged();
        }
        else
        {
            state = EnemyState.Die;
            print("상태전환: AnyState -> Die");

            Die();
        }
    }

    private void Damaged()
    {
        StartCoroutine(DamageProc());
    }

    IEnumerator DamageProc()
    {
        yield return new WaitForSeconds(1.0f);
        state = EnemyState.Move;
        print("상태전환: Damaged -> Move");
    }

    private void Die()
    {
        StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        yield return new WaitForSeconds(2.0f);
        print("죽었다!!!");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //공격범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어 탐지 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //시작지머에서 이동가능한 최대 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPoint, moveRange);


    }

}

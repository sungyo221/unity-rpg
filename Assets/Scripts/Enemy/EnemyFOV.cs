using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : Enemy
{
    [SerializeField] float eFOV = 90f;  //�þ߰�
    [SerializeField] float edetectionRange = 15f;  //���� ����
    [SerializeField] float eMoveSpeed = 2f;
    [SerializeField] float currentBlend = 0f;
    [SerializeField] Transform enemyHPbar;
    [SerializeField] Camera mainCam;
    
    Transform playerTrans;  //�÷��̾� ��ġ    

    private void Awake()
    {        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;        
    }

    private void Update()
    {
        if(enemyHPbar != null)
        {
            EnemyHPbar();
        }        
    }

    private void FixedUpdate()
    {
        if(curHealth > 0)
        {
            DetectPlayer();
            MoveToPlayer();
        }
        if(seePlayer) eMoveing = true;
        else eMoveing = false;
    }

    public void MoveToPlayer()
    {
        if (seePlayer && !eAttack && !iHit && !iDie)
        {
            switch(enemyType)
            {
                case EnemyAttack.Type.Slime:
                    Vector3 direction = (playerTrans.position - transform.position).normalized;

                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4f);

                    transform.position += direction * eMoveSpeed * Time.deltaTime;
                    anim.SetBool("sMove", true);

                    if(!moveSoundCheck)
                    {
                        audioSources[0].Play();
                        moveSoundCheck = true;
                    }                    

                    float moveValue = Mathf.Clamp(eMoveSpeed, 0f, 1f);
                    currentBlend = Mathf.Lerp(currentBlend, moveValue, Time.deltaTime * 0.5f);
                    anim.SetFloat("Blend", currentBlend);
                    break;
                case EnemyAttack.Type.FishManRange:                
                case EnemyAttack.Type.FishManMelee:
                    Vector3 directionFishman = (playerTrans.position - transform.position).normalized;
                    directionFishman.y = 0;

                    Quaternion lookRotationFishman = Quaternion.LookRotation(directionFishman);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationFishman, Time.deltaTime * 4f);

                    transform.position += directionFishman * eMoveSpeed * Time.deltaTime;
                    anim.SetBool("sMove", true);

                    if(!moveSoundCheck)
                    {
                        audioSources[0].Play();
                        moveSoundCheck = true;
                    }
                    break;                
                case EnemyAttack.Type.Dragon:
                    Vector3 directionOther = (playerTrans.position - transform.position).normalized;
                    directionOther.y = 0;

                    Quaternion lookRotationOther = Quaternion.LookRotation(directionOther);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationOther, Time.deltaTime * 4f);

                    transform.position += directionOther * eMoveSpeed * Time.deltaTime;
                    anim.SetBool("sMove", true);
                    if (!moveSoundCheck)
                    {
                        audioSources[0].Play();
                        moveSoundCheck = true;
                    }
                    break;
            }            
        }
        else
        {
            if(moveSoundCheck)
            {
                audioSources[0].Stop();
                moveSoundCheck = false;
            }            
            anim.SetBool("sMove", false);
        }
    }    

    public void DetectPlayer()
    {
        //�÷��̾� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, playerTrans.position);
        //Debug.DrawLine(transform.position, playerTrans.position, Color.red); //������Ʈ�� �÷��̾������Ʈ�� �Ÿ��� ���������� ��Ÿ���°�
        //Debug.Log($"dragon + {distance},{edetectionRange}"); //������Ʈ�� �÷��̾��� �Ÿ�, ���������� �α��ϴ°�
        switch(enemyType)
        {
            case EnemyAttack.Type.FishManMelee:
                if (distance <= edetectionRange)
                {
                    if(!eAttack)
                    {
                        //�þ߰� �ȿ� �ִ��� Ȯ��
                        Vector3 directionPlayer = (playerTrans.position - transform.position).normalized;

                        float angle = Vector3.Angle(transform.forward, directionPlayer);

                        if (angle < eFOV)
                        {
                            seePlayer = true;
                        }                        
                    }                    
                }
                else
                {                    
                    seePlayer = false;
                }
                break;
            case EnemyAttack.Type.FishManRange:            
            case EnemyAttack.Type.Slime:
            case EnemyAttack.Type.Dragon:
                if (distance <= edetectionRange)
                {
                    if(!eAttack)
                    {
                        //�þ߰� �ȿ� �ִ��� Ȯ��
                        Vector3 directionPlayer = (playerTrans.position - transform.position).normalized;

                        float angle = Vector3.Angle(transform.forward, directionPlayer);

                        if (angle < eFOV)
                        {
                            //Debug.Log($"SeePlayerTrue{gameObject.name}");                            
                            seePlayer = true;
                        }                        
                    }
                    else
                    {
                        //Debug.Log($"SeePlayereAttackFalse{gameObject.name}");
                    }
                }
                else
                {

                    //Debug.Log($"SeePlayerDistanceflase{gameObject.name},{distance}/{edetectionRange}");                    
                    seePlayer = false;
                }
                break;
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, edetectionRange); // ���� ������ �������� ��ȭ�鿡 ��Ÿ���� �ϴ°�
    }

    void EnemyHPbar()
    {
        Quaternion q_hp = Quaternion.LookRotation(enemyHPbar.position - mainCam.transform.position);
        Vector3 hp_angle = Quaternion.RotateTowards(enemyHPbar.rotation, q_hp, 200).eulerAngles;
        enemyHPbar.rotation = Quaternion.Euler(0, hp_angle.y, 0);
    }
}

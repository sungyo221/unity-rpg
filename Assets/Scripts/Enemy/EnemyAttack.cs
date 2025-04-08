using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : Enemy
{    
    [SerializeField] float eAttackRange = 1f;  //공격 범위      
    [SerializeField] float attackDuration = 2.5f;
    float bossAttackRamdomTime;
    [SerializeField] GameObject damageBox;  // 데미지를 주는 콜라이더       

    public GameObject rockPrefab;  // 돌 프리펩 
    public GameObject throwPoint;
    [SerializeField] float throwForce = 10f;  //던지는 힘

    public GameObject MagicBallPrefab;
    [SerializeField] float magicSpeed = 4f;

    public GameObject fireBreathPrefab;

    Transform playerTrans;  //플레이어 위치
    public Transform rockTrans;  //돌 스폰 위치    
    public Transform fireBreathTrans;

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        switch (enemyType)
        {
            case Type.FishManRange:
                rockTrans = GameObject.FindGameObjectWithTag("Rock").transform;
                break;            
        }
    }

    private void Update()
    {
        if(!eAttack && enemyType == Type.Dragon)
        {
            audioSources[1].Stop();
            audioSources[2].Stop();
        }
    }

    public void Attack()
    {       
        Player player = playerTrans.GetComponent<Player>();        
        if (player != null && !eAttack && !iHit && !iDie)
        {
            switch(enemyType)
            {
                case Type.Slime:
                case Type.FishManMelee:
                    anim.SetTrigger("sAttack");
                    StartCoroutine(ResetAtt());
                    break;
                case Type.FishManRange:
                    ThrowRock();
                    break;                
                case Type.Dragon:
                    if(Time.time >= bossAttackRamdomTime)
                    {
                        StartCoroutine(BossRandomAttack());
                    }                    
                    break;                
            }
        }
    }

    void ThrowRock()
    {
        float distance = Vector3.Distance(transform.position, playerTrans.position);
        if(distance <= eAttackRange && !iHit && !eAttack && !iDie)
        {
            anim.SetTrigger("sAttack");
            eAttack = true;
            StartCoroutine(Throw());            
        }
    }

    IEnumerator Throw()
    {
        yield return new WaitForSeconds(1.1f);

        if (!iHit)
        {
            yield return new WaitForSeconds(1.3f);
            audioSources[1].Play();
            GameObject rock = Instantiate(rockPrefab, rockTrans.position, Quaternion.identity);
            Rigidbody rigid = rock.GetComponent<Rigidbody>();

            Vector3 direction = (throwPoint.transform.position - rockTrans.position).normalized;
            float distance = Vector3.Distance(rockTrans.position, throwPoint.transform.position);

            Vector3 force = direction * throwForce;
            rigid.AddForce(force, ForceMode.Impulse);            
        }        

        yield return new WaitForSeconds(4.6f);
        eAttack = false;
    }    

    IEnumerator ResetAtt()
    {
        if(!iHit && !eAttack && !iDie)
        {
            switch(enemyType)
            {
                case Type.Slime:
                    eAttack = true;
                    yield return new WaitForSeconds(1.1f);
                    if (!iHit)
                    {
                        damageBox.SetActive(true);
                        audioSources[1].Play();
                        yield return new WaitForSeconds(0.1f);
                        damageBox.SetActive(false);
                        yield return new WaitForSeconds(attackDuration);
                        eAttack = false;
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case Type.FishManMelee:
                    eAttack = true;
                    yield return new WaitForSeconds(1f);
                    if (!iHit)
                    {
                        damageBox.SetActive(true);
                        audioSources[1].Play();
                        yield return new WaitForSeconds(0.1f);
                        damageBox.SetActive(false);
                        yield return new WaitForSeconds(attackDuration);
                        eAttack = false;
                        boxCollider.enabled = true;                        
                    }
                    break;
                case Type.Dragon:
                    eAttack = true;
                    audioSources[1].Play();
                    yield return new WaitForSeconds(1f);
                    damageBox.SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                    damageBox.SetActive(false);
                    yield return new WaitForSeconds(0.8f);
                    eAttack = false;                   
                    break;
            }
        }        
    }

    IEnumerator BossRandomAttack()
    {
        int attackType = Random.Range(0, 3);
        BossAttackType(attackType);

        float waitTime = Random.Range(5f, 8f); 
        bossAttackRamdomTime = Time.time + waitTime;
        yield return null;
    }

    void BossAttackType(int attackType)
    {
        float meleeAttack = 4f;
        float meleeDistance = Vector3.Distance(transform.position, playerTrans.position);
        if(meleeDistance <= meleeAttack)
        {
            meleeRange = true;
        }
        else
        {
            meleeRange = false;
        }

        float breathRange = 8f;
        float rangeDistance = Vector3.Distance(transform.position, playerTrans.position);
        switch (attackType)
        {
            case 0:
                if(meleeRange && !eAttack && seePlayer && !iDie && !iHit)
                {
                    BossMeleeAttack();                    
                }
                break;
            case 1:
                if(rangeDistance <= breathRange && !eAttack && seePlayer && !iDie && !iHit)
                {
                    BossFireBreath();
                }
                break;
            case 2:
                if(rangeDistance <= breathRange && !eAttack && seePlayer && !iDie && !iHit)
                {
                    BossFireBreathAround();
                }
                break;
        }
    }

    void BossMeleeAttack()
    {        
        anim.SetTrigger("sAttack");
        StartCoroutine(ResetAtt());
    }

    void BossFireBreath()
    {
        float breathRange = 8f;
        float distance = Vector3.Distance(transform.position, playerTrans.position);
        if(distance <= breathRange && !eAttack)
        {
            anim.SetTrigger("sFire");
            StartCoroutine(FireBreath());
        }
    }

    void BossFireBreathAround()
    {
        anim.SetTrigger("sFire2");
        StartCoroutine(FireBreath2());
    }

    IEnumerator FireBreath()
    {
        eAttack = true;
        yield return new WaitForSeconds(1f);
        audioSources[2].Play();
        GameObject bossFireBreath = Instantiate(fireBreathPrefab, fireBreathTrans.position, Quaternion.identity);
        bossFireBreath.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        yield return new WaitForSeconds(2f);
        Destroy(bossFireBreath);        
        audioSources[2].Stop();
        yield return new WaitForSeconds(1f);
        eAttack = false;
    }

    IEnumerator FireBreath2()
    {
        eAttack = true;
        yield return new WaitForSeconds(1.5f);
        audioSources[2].Play();
        GameObject bossFireBreath = Instantiate(fireBreathPrefab, fireBreathTrans.position, Quaternion.identity);        
        StartCoroutine(BreathTurn(bossFireBreath));        
        yield return new WaitForSeconds(1.7f);
        Destroy(bossFireBreath);        
        audioSources[2].Stop();
        yield return new WaitForSeconds(1f);
        eAttack = false;

    }

    IEnumerator BreathTurn(GameObject particle)
    {
        while(particle != null)
        {
            particle.transform.position = fireBreathTrans.position;
            particle.transform.rotation = Quaternion.LookRotation(fireBreathTrans.forward);
            yield return null;
        }
    }
}

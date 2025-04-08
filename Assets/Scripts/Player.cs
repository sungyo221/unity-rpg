using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : character
{    
    public GameObject damageBox;    
    public Camera mainCamera;    

    public SkillQuickSlot skillQuickSlot;

    public GameObject iceAttack;
    public GameObject iceRain;
    public GameObject fireBall;
    public GameObject fireRain;

    public Transform fireBallSpawnPoint;
    public Transform iceRainSpawnPoint;
    public Transform fireRainSpawnPoint;

    Vector3 movement;
    
    public int money = 10000;    
    public int level = 1;
    public float curSkillCoolTimeQ = 90;
    public float curSkillCoolTimeE = 120;

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float dodgeSpeed = 5f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float attackDuration = 0.8f;    

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();        
    }

    private void Start()
    {
        money = 10000;
        PlayerHealth();
    }

    private void Update()
    {        
        AnimationState();
        Attack();
        Dodge();
        RecorvingStatus();
        LevelUp();
        SkillCoolTime();
        SkillAction();      
        if (Input.GetKeyDown(KeyCode.M))
        {
            iDie = true;
        }
        //Debug.DrawRay(this.transform.position, rigid.velocity,Color.red); 
        //Debug.DrawRay(this.transform.position, mainCamera.transform.forward, Color.blue); 카메라가 바라보는 방향 파란선
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        Move();
    }

    void RecorvingStatus()
    {
        if (!iDodge && !iAttack && !iDie && !iStaminaRecovering)
        {
            StartCoroutine(RecoveryStamina());
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    void SkillCoolTime()
    {
        if (curSkillCoolTimeQ <= 90)
        {
            curSkillCoolTimeQ += Time.deltaTime;
            if (curSkillCoolTimeQ == 90)
            { curSkillCoolTimeQ = 90; }
        }
        if (curSkillCoolTimeE <= 120)
        {
            curSkillCoolTimeE += Time.deltaTime;
            if (curSkillCoolTimeE == 120)
            { curSkillCoolTimeE = 120; }
        }
    }

    void Move()
    {
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        movement = new Vector3(hori, 0, verti).normalized;       

        if(movement.magnitude > 0 && !iAttack && !iDodge && !iHit && !iDie)
        {            
            rigid.velocity = Vector3.zero;
            //rigid.velocity = movement * moveSpeed;
            transform.Translate(movement * moveSpeed * Time.deltaTime);
            iMove = true;
        }
        else
        {            
            iMove = false;
        }
    }

    void PlayerRotation()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        transform.LookAt(transform.position + cameraForward);
    }

    void Dodge()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !iAttack && !iHit && !iDie)
        {
            anim.SetTrigger("isDodge");
            iDodge = true;
            curStamina -= 10;            
            StartCoroutine(ResetDodge());
        }
    }

    IEnumerator ResetDodge()
    {
        Vector3 dodgeDirection = movement.normalized;
        //rigid.velocity = dodgeDirection * dodgeSpeed;
        rigid.AddRelativeForce(movement * dodgeSpeed, ForceMode.Impulse);
        iInvincibility = true;
        yield return new WaitForSeconds(dodgeDuration);
        iInvincibility = false;
        iDodge = false;
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !iDodge && !iAttack && !iDie)
        {
            GameManager.GetInstance().soundManager.audioSources[1].Play();
            anim.SetTrigger("isMeleeAttack");
            iAttack = true;
            curStamina -= 10;
            StartCoroutine(ResetAttack());
            StartCoroutine(CritChance());
        }
    }

    IEnumerator ResetAttack()
    {        
        damageBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        damageBox.SetActive(false);
        iAttack=false;
    }

    IEnumerator CritChance()
    {
        float randomValue = Random.Range(0, 100);
        if(randomValue <= crit)
        {
            critChance = true;
            if(critChance)
            {                
                damage *= 2;
                Debug.Log($"Player + {damage}");
                yield return new WaitForSeconds(attackDuration);
                damage /= 2;
                critChance = false;
            }
        }        
    }

    void AnimationState()
    {
        if(Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.z,0))
        {
            anim.SetBool("isMove", false);
        }
        else
        {
            anim.SetBool("isMove", true);
        }
        anim.SetFloat("xDir", movement.x);
        anim.SetFloat("yDir", movement.z);
    }

    IEnumerator RecoveryStamina()
    {
        yield return new WaitForSeconds(1.2f);

        if(!iDodge && !iAttack && !iDie)
        {
            iStaminaRecovering = true;

            while (curStamina <= maxStamina)
            {
                curStamina += 10 * Time.deltaTime;
                curStamina = Mathf.Clamp(curStamina, 0, maxStamina);

                yield return new WaitForSeconds(1f);
            }
        }        

        iStaminaRecovering = false;
    }

    public void AddExp(int exp)
    {
        curExp += exp;
    }

    public void LevelUp()
    {
        if (curExp >= maxExp)
        {
            GameManager.GetInstance().soundManager.audioSources[5].Play();
            level++;
            curExp %= maxExp;
            maxExp += 10;
            GameManager.GetInstance().guiManager.level.text = "Lv " + level;
            maxHealth += 10;
            maxStamina += 5;
            damage += 3;
            defence += 2;
            crit += 2;
        }
    }    

    public void SkillAction()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (skillQuickSlot.skillQuick[0].skillType == SkillManager.SKILL_TYPE.IceAttack)
            {
                Skill skill = iceAttack.GetComponent<Skill>();
                if(skill.skillCoolTime <= curSkillCoolTimeQ)
                {
                    GameManager.GetInstance().soundManager.audioSources[6].Play();
                    StartCoroutine(SetActiveSkillQ());
                }                
            }
            else if (skillQuickSlot.skillQuick[0].skillType == SkillManager.SKILL_TYPE.FireBall)
            {
                Skill skill = fireBall.GetComponent<Skill>();
                if(skill.skillCoolTime <= curSkillCoolTimeQ)
                {                    
                    StartCoroutine(SetActiveSkillQ());
                }                
            }    
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (skillQuickSlot.skillQuick[1].skillType == SkillManager.SKILL_TYPE.IceRain)
            {                
                Skill skill = iceRain.GetComponent<Skill>();
                if(skill.skillCoolTime < curSkillCoolTimeE)
                {                    
                    StartCoroutine(SetActiveSkillE());
                }                
            }
            if(skillQuickSlot.skillQuick[1].skillType == SkillManager.SKILL_TYPE.FireRain)
            {
                Skill skill = fireRain.GetComponent<Skill>();
                if(skill.skillCoolTime < curSkillCoolTimeE)
                {
                    StartCoroutine (SetActiveSkillE());
                }
            }
        }
    }

    IEnumerator SetActiveSkillQ()
    {
        if (skillQuickSlot.skillQuick[0].skillType == SkillManager.SKILL_TYPE.IceAttack)
        {
            curSkillCoolTimeQ = 0;
            iceAttack.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            iceAttack.SetActive(false);            
        }
        if (skillQuickSlot.skillQuick[0].skillType == SkillManager.SKILL_TYPE.FireBall)
        {
            curSkillCoolTimeQ = 0;
            Instantiate(fireBall, fireBallSpawnPoint.position, Quaternion.identity);            
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SetActiveSkillE()
    {
        if (skillQuickSlot.skillQuick[1].skillType == SkillManager.SKILL_TYPE.IceRain)
        {            
            curSkillCoolTimeE = 0;
            Skill skill = iceRain.GetComponent<Skill>();
            Instantiate(iceRain, iceRainSpawnPoint.position, Quaternion.identity);
            yield return null;
        }
        else if (skillQuickSlot.skillQuick[1].skillType == SkillManager.SKILL_TYPE.FireRain)
        {
            curSkillCoolTimeE = 0;
            Skill skill = fireRain.GetComponent<Skill>();
            Instantiate(fireRain, fireRainSpawnPoint.position, Quaternion.identity);
            yield return null;
        }
    }
}

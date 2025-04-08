using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : SkillManager
{   
    Enemy enemy;
    Dummy dummy;
    public int skillDamage;
    public int skillCoolTime;
    public int skillDuration;
    public float curSkillDuration;

    private void Start()
    {
        SkillAssignment();
        ShotFireBall();
        SpawnIceRain();
        SpawnFireRain();
    }

    private void Update()
    {
        
    }

    public void SkillAssignment()
    {
        switch(skillType)
        {
            case SkillManager.SKILL_TYPE.IceAttack:
                skillDamage = listSkillInfo[1].skillDamage;
                skillCoolTime = listSkillInfo[1].skillCoolTime;
                break;
            case SkillManager.SKILL_TYPE.IceRain:
                skillDamage = listSkillInfo[2].skillDamage;
                skillCoolTime = listSkillInfo[2].skillCoolTime;
                skillDuration = listSkillInfo[2].skillDuration;
                break;
            case SkillManager.SKILL_TYPE.FireBall:
                skillDamage = listSkillInfo[3].skillDamage;
                skillCoolTime = listSkillInfo[3].skillCoolTime;
                break;            
            case SkillManager.SKILL_TYPE.FireRain:
                skillDamage = listSkillInfo[4].skillDamage;
                skillCoolTime = listSkillInfo[4].skillCoolTime;
                skillDuration = listSkillInfo[4].skillDuration;
                break;
        }
    }

    public void ShotFireBall()
    {
        if(skillType == SKILL_TYPE.FireBall)
        {            
            Rigidbody rigid = GetComponent<Rigidbody>();
            Vector3 force = Vector3.forward * 3;
            rigid.AddForce(force, ForceMode.Impulse);
            StartCoroutine(RemoveFireBall());
        }
    }

    IEnumerator RemoveFireBall()
    {
        GameManager.GetInstance().soundManager.audioSources[7].Play();
        yield return new WaitForSeconds(5f);
        GameManager.GetInstance().soundManager.audioSources[7].Stop();
        yield return null;
        Destroy(gameObject);
    }

    public void SpawnIceRain()
    {
        if(skillType == SKILL_TYPE.IceRain) StartCoroutine(RemoveIceRain());
    }

    IEnumerator RemoveIceRain()
    {
        GameManager.GetInstance().soundManager.audioSources[8].Play();
        yield return new WaitForSeconds(10f);
        GameManager.GetInstance().soundManager.audioSources[8].Stop();
        yield return null;
        Destroy(gameObject);
    }

    public void SpawnFireRain()
    {
        if(skillType == SKILL_TYPE.FireRain) StartCoroutine(RemoveFireRain());
    }

    IEnumerator RemoveFireRain()
    {
        GameManager.GetInstance().soundManager.audioSources[9].Play();
        yield return new WaitForSeconds(10f);
        GameManager.GetInstance().soundManager.audioSources[9].Stop();
        yield return null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (skillType == SKILL_TYPE.IceAttack || skillType == SKILL_TYPE.FireBall)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(skillDamage);
                    if(skillType == SKILL_TYPE.FireBall)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if(other.gameObject.CompareTag("Dummy"))
            {
                dummy = other.GetComponent<Dummy>();
                if(dummy != null)
                {
                    dummy.KnockBack();
                    if (skillType == SKILL_TYPE.FireBall)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(skillType == SKILL_TYPE.IceRain || skillType == SKILL_TYPE.FireRain)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(skillDamage);
                }
            }
            else if (other.gameObject.CompareTag("Dummy"))
            {
                Dummy dummy = other.GetComponent<Dummy>();
                if (dummy != null)
                {
                    dummy.KnockBack();
                }
            }
        }        
    }        
}

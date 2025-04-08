using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [Header("character status")]
    public float curHealth;
    public float maxHealth;    
    public float curStamina;
    public float maxStamina;
    public float curExp;
    public float maxExp;
    public int enemyExp;
    public int damage;
    public int defence = 0;
    public float crit = 0;

    [Header("character condition")]
    public bool iMove;
    public bool iAttack;
    public bool iDodge;
    public bool iHit;
    public bool iDie = false;
    public bool iInvincibility = false;
    public bool iStaminaRecovering = false;
    public bool critChance = false;

    [Header("Enemy Condition")]
    public bool eBlock = false;
    public bool eAttack = false;
    protected bool meleeRange = false;
    protected bool attackSoundCheck = false;
    public bool seePlayer = false;  //플레이어 볼 수 있는지 여부
    protected bool moveSoundCheck = false;
    public bool eMoveing = false;
    public enum Type { Slime, FishManRange, FishManMelee, FlyingEye, Dragon }
    public Type enemyType;
    protected Animator anim;
    protected Rigidbody rigid;

    AudioSource[] audioSources;

    private void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        audioSources = enemy.audioSources;
    }

    protected void PlayerHealth()
    {
        maxHealth = 100;
        curHealth = maxHealth;
        maxStamina = 100;
        curStamina = maxStamina;
        curExp = 0;
        maxExp = 100;
        damage = 10;
    }

    public void TakeDamage(int damage)
    {
        if (!iHit && !iInvincibility && !iDie && gameObject.CompareTag("Player"))
        {
            curHealth -= (damage - defence);
            anim.SetTrigger("isHit");
            GameManager.GetInstance().soundManager.audioSources[4].Play();
            iHit = true;
            StartCoroutine(ResetTakeDamage());
            if (curHealth <= 0)
            {
                anim.SetTrigger("iDie");
                iDie = true;
            }
        }
        if (!iHit && !iDie)
        {
            iHit = true;
            if (enemyType == EnemyAttack.Type.FishManMelee)
            {
                curHealth -= damage;
                audioSources[2].Play();
                anim.SetTrigger("sHit");
                StartCoroutine(ResetTakeDamage());

            }
            else if (enemyType == EnemyAttack.Type.Dragon)
            {
                curHealth -= damage;
                audioSources[3].Play();
                anim.SetTrigger("sHit");
                StartCoroutine(ResetTakeDamage());
            }
            else
            {
                curHealth -= damage;
                audioSources[2].Play();
                anim.SetTrigger("sHit");
                StartCoroutine(ResetTakeDamage());
            }

            if (curHealth <= 0)
            {
                GameManager.GetInstance().player.AddExp(enemyExp);
                anim.SetTrigger("sDie");
                StartCoroutine(Die());
            }
            StartCoroutine(ResetTakeDamage());
        }
    }

    IEnumerator ResetTakeDamage()
    {
        if(gameObject.CompareTag("Player"))
        {
            float hitDuration = 0.25f;
            yield return new WaitForSeconds(hitDuration);
            iHit = false;
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            float hitDuration = 1.05f;
            yield return new WaitForSeconds(hitDuration);
            iHit = false;
            eAttack = false;
        }
    }

    IEnumerator Die()
    {
        iDie = true;
        float dieDuration = 5f;
        yield return new WaitForSeconds(dieDuration);
        //gameObject.SetActive(false);        
        Destroy(gameObject);
    }
}

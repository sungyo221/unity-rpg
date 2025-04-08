using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : character
{
    public Transform enemyModel;    
    public BoxCollider boxCollider;
    public EnemySpawn enemyspawn;
    EnemyAttack enemyAttack;

    public AudioSource[] audioSources;    

    enum actinoType { Attack, Defend };
    actinoType currentAction;
   
    private void Awake()
    {        
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        switch(enemyType)
        {
            case EnemyAttack.Type.FishManRange:
            case EnemyAttack.Type.FlyingEye:            
                if (!eAttack)
                {
                    enemyAttack.Attack();
                }
                break;
            case EnemyAttack.Type.Dragon:
                if(!eAttack)
                {
                    enemyAttack.Attack();
                }
                break;
        }

        if(enemyModel != transform)
        {
            enemyModel.position = transform.position;
        }        
    }    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && curHealth > 0 && !eAttack)
        {            
            StartCoroutine(ActionEnemy());
        }
    }
    IEnumerator ActionEnemy()
    {
        currentAction = (actinoType)Random.Range(0, 2);
        Debug.Log(currentAction);

        switch (enemyType)
        {
            case EnemyAttack.Type.Slime:
            case EnemyAttack.Type.FishManRange:
            case EnemyAttack.Type.FlyingEye:
            case EnemyAttack.Type.FishManMelee:
                enemyAttack.Attack();
                break;
        }        
        yield return new WaitForSeconds(4f);
    }
}

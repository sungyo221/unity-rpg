using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //public EnemySpawnManager enemySpawnManager;
    public Enemy enemyCheck;
    public EnemySpawnManager.MonsterType monsterType;
    public bool enemySpawning = true;
    public float RespawnCoolTime;
    public float curRespawnCoolTime;

    private void Start()
    {                
        SpawnCoolTime();
        Debug.Log($"EnemySpawn + {gameObject.name}");        
    }

    private void Update()
    {
        if(enemyCheck == null && !enemySpawning)
        {            
            curRespawnCoolTime += Time.deltaTime;
            if (curRespawnCoolTime > RespawnCoolTime)
            {
                SpawnEnemy();
            }
            enemyCheck = GetComponentInChildren<Enemy>();            
        }
        if(enemySpawning)
        {
            enemySpawning = false;
        }
    }

    public void SpawnEnemy()
    {
        if(!enemySpawning)
        {
            Debug.Log($"EnemySpawn.SpawnEnemy() + {gameObject.name}");
            switch(monsterType)
            {
                case EnemySpawnManager.MonsterType.Slime:
                    Instantiate(GameManager.GetInstance().enemySpawnParent.enemySpawnManager[0].enemyPref, transform);
                    break;
                case EnemySpawnManager.MonsterType.RangeFishMan:
                    Instantiate(GameManager.GetInstance().enemySpawnParent.enemySpawnManager[1].enemyPref, transform);
                    break;
                case EnemySpawnManager.MonsterType.MeleeFishMan:
                    Instantiate(GameManager.GetInstance().enemySpawnParent.enemySpawnManager[2].enemyPref, transform);
                    break;                
                case EnemySpawnManager.MonsterType.Dragon:
                    Instantiate(GameManager.GetInstance().enemySpawnParent.enemySpawnManager[3].enemyPref, transform);
                    break;
            }
            //Instantiate(GameManager.GetInstance().enemySpawnManager[].enemyPref, transform);
            enemySpawning = true;
            curRespawnCoolTime = 0;
        }        
    }

    public void SpawnCoolTime()
    {
        switch (monsterType)
        {
            case EnemySpawnManager.MonsterType.Slime:
                RespawnCoolTime = 8f;
                break;
            case EnemySpawnManager.MonsterType.RangeFishMan:
                RespawnCoolTime = 10f;
                break;
            case EnemySpawnManager.MonsterType.MeleeFishMan:
                RespawnCoolTime = 10f;
                break;
            case EnemySpawnManager.MonsterType.Dragon:
                RespawnCoolTime = 180f;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPref;
    public enum MonsterType { Slime, RangeFishMan, MeleeFishMan, FlyingEye, Dragon}
    public MonsterType monsterType;
    public EnemySpawn[] enemySpawnPoint;

    private void Start()
    {
        EnemyAttack enemy = enemyPref.GetComponent<EnemyAttack>();
        monsterType = (MonsterType)enemy.enemyType;
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            enemySpawnPoint[i].monsterType = monsterType;
        }
    }
}

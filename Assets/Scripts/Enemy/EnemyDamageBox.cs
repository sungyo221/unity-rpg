using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageBox : Enemy
{    
    [SerializeField] int eDamage = 10;    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TakeDamage(eDamage);
        }
    }
}

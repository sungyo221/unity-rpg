using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : Player
{
    public int meleeDamage;    
    Enemy enemy;
    Dummy dummy;

    private void Update()
    {
        if(meleeDamage != damage)
        {
            meleeDamage = damage;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            if(enemy != null )
            {
                enemy.TakeDamage(meleeDamage);
            }
        }
        else if(other.gameObject.CompareTag("Dummy"))
        {
            dummy = other.GetComponent<Dummy>();
            if(dummy != null )
            {
                dummy.KnockBack();
            }
        }
    }
}

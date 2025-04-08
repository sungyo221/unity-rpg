using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIreBreath : EnemyAttack
{
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            TakeDamage(10);
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlocking : Enemy
{
    public void Blocking()
    {
        seePlayer = false;
        if(!eAttack)
        {
            anim.SetTrigger("sBlockStart");
            StartCoroutine(BlockingLoop());
        }        
    }

    IEnumerator BlockingLoop()
    {
        if(!eAttack)
        {
            yield return new WaitForSeconds(0.3f);
            eBlock = true;
            anim.SetBool("sBlockLoop", true);
            if (eBlock && iHit)
            {
                audioSources[3].Play();
                anim.SetTrigger("sBlockHit");
            }
            yield return new WaitForSeconds(4f);
            anim.SetTrigger("sBlockEnd");
            eBlock = false;
            boxCollider.enabled = true;            
        }        
    }
}

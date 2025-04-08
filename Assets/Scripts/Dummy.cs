using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void KnockBack()
    {
        anim.SetTrigger("dHit");
        StartCoroutine(ResetKnockBack());
    }

    IEnumerator ResetKnockBack()
    {
        yield return new WaitForSeconds(1f);
    }
}

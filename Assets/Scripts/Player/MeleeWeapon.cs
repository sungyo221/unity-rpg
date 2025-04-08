using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    MeshCollider meshcColl;

    private void Start()
    {
        meshcColl = GetComponentInChildren<MeshCollider>();
    }

    public void Damage()
    {
        StartCoroutine(AttackDamage());
    }

    IEnumerator AttackDamage()
    {
        meshcColl.enabled = true;
        yield return new WaitForSeconds(0.25f);
        meshcColl.enabled = false;
    }
}

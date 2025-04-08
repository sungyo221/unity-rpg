using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public Transform target;
    public GameObject magicBall;
    public GameObject hitEffect;
    [SerializeField] float magicSpeed = 3f;

    Player player;

    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * magicSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            player.TakeDamage(10);
            StartCoroutine(OnEffect());
        }
    }

    IEnumerator OnEffect()
    {
        magicBall.SetActive(false);
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

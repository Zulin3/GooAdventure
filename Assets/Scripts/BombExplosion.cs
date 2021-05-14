using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private float timeDelay = 5;
    [SerializeField] private float explosionRadius = 5;
    [SerializeField] private float explosionDamage = 50;
    [SerializeField] private GameObject explosion;
	
    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeDelay);
        var boom = Instantiate(explosion,transform.position,transform.rotation);
        boom.transform.parent = gameObject.transform.parent;
        Collider[] hitColliders = Physics.OverlapSphere(boom.transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject);
            if (hitCollider.gameObject.tag == "Enemy")
            {
                var enemy = hitCollider.gameObject.GetComponent<Damageable>();
                enemy.dealDamage(explosionDamage);
            }
                
        }
        Destroy(gameObject);
    }
}

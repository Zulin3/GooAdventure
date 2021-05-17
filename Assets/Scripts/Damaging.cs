using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private bool playerOwned = false;
    [SerializeField] private bool oneTime = true;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip damageSound;

    public void Awake()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && playerOwned)
            return;

        if ((collider.gameObject.tag == "Enemy" && playerOwned) || (collider.gameObject.tag == "Player" && !playerOwned))
        {
            Damageable dmgable = collider.gameObject.GetComponent<Damageable>();
            dmgable.dealDamage(damage);
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
            if (oneTime)
            {
                Destroy(gameObject);
            }
        }
        
        if (oneTime)
        {
            Destroy(gameObject,3);
        }
    }
}

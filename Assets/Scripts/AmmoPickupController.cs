using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    [SerializeField] private int ammoIncrease = 5;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().addAmmo(ammoIncrease);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}

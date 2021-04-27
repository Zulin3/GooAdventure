using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    private bool pulled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !pulled)
        {
            Animation anim = objectToActivate.GetComponent<Animation>();

            GetComponent<Animation>().Play("PullLever");
            anim.Play("SecretDoorOpen");
            pulled = true;
        }
    }
}

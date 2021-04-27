using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject);
            var anim = trapDoor.GetComponent<Animation>();
            Debug.Log(anim);
            anim.Play("TrapRotate");
        }
    }
}

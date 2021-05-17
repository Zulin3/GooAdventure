using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAggroRadius : MonoBehaviour
{

    private TurretController turret;

    void Awake()
    {
        turret = transform.parent.gameObject.GetComponent<TurretController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turret.PlayerEntered(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turret.PlayerExited(other);
        }
    }
}

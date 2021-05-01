using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(rotateSpeed, 0f, 0f));
    }
}

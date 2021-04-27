using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private Vector3 _target;

    public Vector3 Target {
        set => _target = value;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        if (transform.position == _target)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

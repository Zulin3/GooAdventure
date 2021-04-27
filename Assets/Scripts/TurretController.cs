using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3f;
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;

    private GameObject _target;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void startShooting()
    {
        animator.SetBool("Attacking", true);
        StartCoroutine(Shoot());
    }

    private void stopShooting()
    {
        animator.SetBool("Attacking", false);
        StopCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            Vector3 shotTarget = _target.transform.position;
            shotTarget += Vector3.up;
            var newShot = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            newShot.GetComponent<ShotMover>().Target = shotTarget;
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _target = other.gameObject;
            startShooting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _target = null;
            stopShooting();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3f;
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject eye;
    [SerializeField] private Transform shotSpawn;

    private GameObject _target;
    private bool _dead = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool Dead
    {
        set
        {
            _dead = value;
        }
    }

    void Update()
    {
        if (animator.GetBool("Dead") && !_dead)
        {
            _dead = true;
            stopShooting();
        }
        
        if (_target != null && !_dead)
        {
            Vector3 direction = _target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void startShooting()
    {
        animator.SetBool("Attacking", true);
        StopCoroutine(Shoot());
        StartCoroutine(Shoot());
    }

    private void stopShooting()
    {
        animator.SetBool("Attacking", false);
        StopCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (!_dead)
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(eye.transform.position, eye.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

            if (isHit && hit.collider.tag == "Player")
            {

                Vector3 shotTarget = _target.transform.position;
                shotTarget += Vector3.up;
                var newShot = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                newShot.GetComponent<ShotMover>().Target = shotTarget;
                yield return new WaitForSeconds(shootDelay);
            }
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_dead)
        {
            _target = other.gameObject;
            startShooting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_dead)
        {
            _target = null;
            stopShooting();
        }
    }
}

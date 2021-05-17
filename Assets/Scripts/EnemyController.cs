using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float recountPathDelay = 1;
    [SerializeField] private float rotateSpeed = 3;
    [SerializeField] private GameObject eye;
    [SerializeField] private GameObject damageArea;
    private NavMeshAgent agent;
    private GameObject player;
    private bool _dead = false;
    private Animator animator;

    public bool Dead
    {
        set
        {
            _dead = value;
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(RecountPath());
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);

            RaycastHit hit;
            bool isHit = Physics.Raycast(eye.transform.position, eye.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

            if (isHit && hit.collider.tag == "Player")
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    public void Attack()
    {
        damageArea.SetActive(true);
    }

    public void NoAttack()
    {
        damageArea.SetActive(false);
    }

    private IEnumerator RecountPath()
    {
        while (!_dead)
        {
            agent.SetDestination(player.GetComponent<Transform>().position);
            yield return new WaitForSeconds(recountPathDelay);
        }
    }

}

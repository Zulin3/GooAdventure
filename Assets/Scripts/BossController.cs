using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform eye;
    [SerializeField] private float rotateSpeed = 2;
    [SerializeField] private float recountPathDelay = 1f;
    [SerializeField] private float stopFollowingDelay = 5f;
    [SerializeField] private GameObject damageArea;

    private bool _dead = false;
    private Animator animator;
    private bool followingPlayer = false;
    private Transform player;
    private NavMeshAgent agent;


    public bool Dead
    {
        set
        {
            _dead = value;
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(eye.position, eye.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

        if (isHit)
        {
            if (!followingPlayer && hit.collider.tag == "Player")
            {
                followingPlayer = true;
                GetComponent<PatrolEnemy>().Patrolling = false;
                StartCoroutine(RecountPath());
                StopCoroutine(StopFollowingAfterDelay());
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    animator.SetTrigger("Attack");
                    animator.SetBool("Attacking",true);
                }
            }

            if (followingPlayer && hit.collider.tag != "Player")
            {
                StartCoroutine(StopFollowingAfterDelay());
            }
        }
    }

    private IEnumerator StopFollowingAfterDelay()
    {
        Debug.Log("Stopping following after 5s");
        yield return new WaitForSeconds(stopFollowingDelay);
        Debug.Log("Stopped following");
        StopCoroutine(RecountPath());
        followingPlayer = false;
        GetComponent<PatrolEnemy>().Patrolling = true;
    }

    private IEnumerator RecountPath()
    {
        while (!_dead && followingPlayer)
        {
            Debug.Log("Started Following!");
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(recountPathDelay);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float recountPathDelay = 1;
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
            if (!animator.GetBool("Attacking"))
            {
                animator.SetBool("Attacking", true);
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
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

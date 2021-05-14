using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] trajectory;
    private NavMeshAgent agent;
    private Animator animator;
    private int nextTarget = 0;
    private bool _patrolling = true;
    public bool Patrolling
    {
        set
        {
            _patrolling = value;
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(trajectory[0].position);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("Dead") && _patrolling && (agent.remainingDistance < agent.stoppingDistance))
        {
            nextTarget = (nextTarget + 1) % trajectory.Length;
            agent.SetDestination(trajectory[nextTarget].position);
        }
    }
}

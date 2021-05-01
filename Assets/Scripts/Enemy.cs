using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float hitPoints = 100;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ApplyDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints < 0)
        {
            animator.SetTrigger("Die");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            GetComponent<EnemyController>().Dead = true;
        }
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}

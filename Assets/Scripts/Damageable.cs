using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthBar;
    private float health;
    private Animator animator;

    void Awake()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void dealDamage(float dmg)
    {
        health -= dmg;
        healthBar.value = health;

        if (health <= 0)
        {
            animator.SetTrigger("Die");
            var agent = GetComponent<NavMeshAgent>();
            if (agent)
            {
                agent.SetDestination(transform.position);
            }
            if (gameObject.tag == "Player")
            {
                var gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                gameController.Lose();
            }
            else if (gameObject.name == "Boss")
            {
                var gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                gameController.Win();
            }
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public void StayDead()
    {
        animator.SetBool("Dead", true);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float recountPathDelay = 1;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(RecountPath());
    }

    private IEnumerator RecountPath()
    {
        while (true)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            agent.SetDestination(player.GetComponent<Transform>().position);
            yield return new WaitForSeconds(recountPathDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

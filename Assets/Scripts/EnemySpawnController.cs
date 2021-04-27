using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float timeDelay = 1;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}

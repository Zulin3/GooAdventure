using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject bombSpawner;
    [SerializeField] private float throwPower;
    [SerializeField] private int ammo = 5;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        
    }

    public void addAmmo(int amount)
    {
        ammo += amount;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float strafe = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(move, 0, strafe);
        //animator.SetFloat("Speed", direction.magnitude);
        var moveVector = direction * moveSpeed * Time.deltaTime;
        transform.Translate(moveVector);

        float rotate = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotate*rotateSpeed, 0);

        if ((ammo > 0) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Throw");
            var newBomb = Instantiate(bomb, bombSpawner.transform.position, bombSpawner.transform.rotation);
            var rb = newBomb.GetComponent<Rigidbody>();
            rb.velocity = newBomb.transform.forward * throwPower;
            ammo--;
        }
        else if (Input.GetAxis("Fire3") > 0)
        {
            animator.SetTrigger("Shoot");
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            animator.SetTrigger("Attack");
        }


    }
}

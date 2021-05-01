using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Mathf;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform bombSpawner;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private float throwBombPower;
    [SerializeField] private float throwProjectilePower;
    [SerializeField] private float jumpForce;
    [SerializeField] private int ammo = 5;

    Animator animator;
    Rigidbody rb;
    bool jumping = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
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
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            animator.SetTrigger("Shoot");
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!Mathf.Approximately(Input.GetAxis("Jump"), 0f) && collision.collider.gameObject.tag == "Floor")
        {
            animator.SetTrigger("Jump");
            jumping = true;
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumping)
        {
            animator.SetTrigger("Land");
            jumping = false;
        }
    }

    public void ThrowBomb(GameObject bomb)
    {
        var newBomb = Instantiate(bomb, bombSpawner.position, bombSpawner.rotation);
        var rb = newBomb.GetComponent<Rigidbody>();
        rb.velocity = newBomb.transform.forward * throwBombPower;
        ammo--;
    }

    void Shoot(GameObject projectile)
    {
        var newProjectile = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
        var rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = newProjectile.transform.forward * throwProjectilePower;
    }
}

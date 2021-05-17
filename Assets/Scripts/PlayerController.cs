using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpMoveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float cameraRotateSpeed;
    [SerializeField] private Transform bombSpawner;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private float throwBombPower;
    [SerializeField] private float throwProjectilePower;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject camera;
    [SerializeField] private int ammo = 5;
    [SerializeField] private GameObject damageArea;
    [SerializeField] private Text bombCounterText;
    [SerializeField] private AudioClip swingSound;

    private Animator animator;
    private Rigidbody rb;
    private AudioSource audio;
    bool jumping = false;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    public void addAmmo(int amount)
    {
        ammo += amount;
        bombCounterText.text = ammo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Dead"))
        {
            return;
        }
        float move = Input.GetAxis("Horizontal");
        float strafe = Input.GetAxis("Vertical");
        float sprint = Input.GetAxis("Sprint");

        
        Vector3 direction = new Vector3(move, 0, strafe);
        var tempRotation = camera.transform.rotation;
        Vector3 movingTo = camera.transform.right * move + camera.transform.forward * strafe;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, movingTo, rotateSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
        camera.transform.rotation = tempRotation;

        if (jumping)
        {
            var moveVector = transform.forward * jumpMoveSpeed * Time.deltaTime;
            transform.Translate(moveVector);
        }

        if (!Mathf.Approximately(direction.magnitude, 0f))
        {
            animator.SetBool("Walk", true);
            if (Mathf.Approximately(sprint, 1f))
            {
                animator.SetBool("Sprint", true);
            }
            else
            {
                animator.SetBool("Sprint", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        float rotate = Input.GetAxis("Mouse X");
        camera.transform.Rotate(0, rotate * cameraRotateSpeed, 0);

        if ((ammo > 0) && Input.GetKeyDown(KeyCode.Mouse2))
        {
            animator.SetTrigger("Throw");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Shoot");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
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
        bombCounterText.text = ammo.ToString();
    }

    void Shoot(GameObject projectile)
    {
        var newProjectile = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
        var rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = newProjectile.transform.forward * throwProjectilePower;
    }

    public void Attack()
    {
        damageArea.SetActive(true);
        audio.clip = swingSound;
        audio.Play();
    }

    public void NoAttack()
    {
        damageArea.SetActive(false);
    }
}

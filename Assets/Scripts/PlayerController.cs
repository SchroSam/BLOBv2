using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Experiment : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Normal movement speed
    public float dashSpeed = 15f;      // Speed during dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    public float dashCooldown = 1f;    // Time before you can dash again
    public int armCount = 0; // Number of arms
    public int legCount = 0; // Number of Legs
    public int batCount = 0; // Number of batteries
    private Rigidbody2D rb; // for getting rigid body
    private float moveInput; 
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDash = -999f;
    public GameObject armShot;
    public GameObject batShot;
    public GameObject legShot;
    private int dire = 0;
    private Component mik;
    public int playerhealth;
    public float healtime;
    public float invistime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get left/right input (-1 to 1)
        moveInput = Input.GetAxisRaw("Horizontal");
        if (playerhealth != 4)
        {
            healtime += Time.deltaTime;
        }
        if (healtime > 30)
        {
            healtime = 0;
            playerhealth += 1;
        }
        float z = Input.GetAxis("Horizontal");
        if (z != 0)
        {
            if (z < 0)
            {
                dire = 0;
            }
            else
            {
                dire = 1;
            }
        }
        // Dash input (Shift key)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDash + dashCooldown)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDash = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (armCount > 0)
            {
                armCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(armShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbArm();

                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (batCount > 0)
            {
                batCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(batShot, transform.position, Quaternion.identity);


                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (legCount > 0)
            {
                legCount -= 1;
                InventoryManager.Instance.UpdateUIFromPlayer(this);
                GameObject newObject = Instantiate(legShot, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpawnOnPlayer>().KillLimbLeg();

                if (dire > 0)
                {
                    newObject.GetComponent<Fired>().z = 1;
                }
                else
                {
                    newObject.GetComponent<Fired>().z = 0;
                }
            }

        }

    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Dash movement
            rb.linearVelocity = new Vector2(moveInput * (dashSpeed + legCount), rb.linearVelocity.y);
            dashTimeLeft -= Time.fixedDeltaTime;

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            // Normal movement
            rb.linearVelocity = new Vector2(moveInput * (moveSpeed + (legCount / 2)), rb.linearVelocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arm") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            armCount += 1;
            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("FARM") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>());
        }
        if (collision.CompareTag("Leg") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            legCount += 1;
            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("Battery") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            batCount += 1;
            Destroy(collision.gameObject);
            InventoryManager.Instance.UpdateUIFromPlayer(this);
        }
        if (collision.CompareTag("Lattack") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            Destroy(collision);
            playerhealth -= 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            if (isDashing)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>());
            }
            else
            {
                if (collision.GetComponent<ArmEnemyMovement>() != null)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>(), false);
                    collision.GetComponent<ArmEnemyMovement>().modeChange();
                }
                else
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>(), false);
                    collision.GetComponent<LegEnemyMove>().modeChange();
                    collision.GetComponent<Animator>().SetInteger("mode", collision.GetComponent<LegEnemyMove>().mode);
                }
            }
        }
    }
}

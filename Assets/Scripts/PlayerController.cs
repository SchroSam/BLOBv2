using UnityEngine;

public class Experiment : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Normal movement speed
    public float dashSpeed = 15f;      // Speed during dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    public float dashCooldown = 1f;    // Time before you can dash again
    public int armCount = 0;
    public int legCount = 0;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDash = -999f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get left/right input (-1 to 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Dash input (Shift key)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDash + dashCooldown)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDash = Time.time;
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
        if (collision.CompareTag("Arm"))
        {
            armCount += 1;
        }
        if (collision.CompareTag("Leg"))
        {
            legCount += 1;
        }
    }
}

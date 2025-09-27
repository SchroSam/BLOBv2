using UnityEngine;

public class Experiment : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDash = -999f;
    private int dire = 0;

    [Header("Projectiles")]
    public GameObject armShot;
    public GameObject batShot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        float z = Input.GetAxis("Horizontal");
        if (z != 0) dire = z < 0 ? 0 : 1;

        // Dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDash + dashCooldown)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDash = Time.time;
        }

        // Shoot Arm
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (InventoryManager.Instance.UseArms(1)) // Use 1 arm
            {
                FireProjectile(armShot);
            }
        }

        // Shoot Bat (using brains as ammo)
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (InventoryManager.Instance.UseBrains(1))
            {
                FireProjectile(batShot);
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Dash movement
            rb.linearVelocity = new Vector2(moveInput * (dashSpeed + InventoryManager.Instance.legs), rb.linearVelocity.y);
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0) isDashing = false;
        }
        else
        {
            // Normal movement
            rb.linearVelocity = new Vector2(moveInput * (moveSpeed + (InventoryManager.Instance.legs / 2f)), rb.linearVelocity.y);
        }
    }

    private void FireProjectile(GameObject projectile)
    {
        if (projectile == null) return;
        GameObject newObj = Instantiate(projectile, transform.position, Quaternion.identity);
        newObj.GetComponent<Fired>().z = dire > 0 ? 1 : 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arm") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            InventoryManager.Instance.AddArms(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Leg") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            InventoryManager.Instance.AddLegs(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Battery") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            InventoryManager.Instance.AddBrains(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy") && gameObject.GetComponent<CircleCollider2D>().gameObject.name == "Blob 1")
        {
            if (isDashing)
            {
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>());
            }
            else
            {
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), collision.GetComponent<BoxCollider2D>(), false);
            }
        }
    }
}

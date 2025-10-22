using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float leftLimit = -8f;
    public float rightLimit = 8f;
    private bool movingRight = true;

    [Header("Attack Settings")]
    public GameObject bombPrefab;       // Drag your bomb sprite here
    public GameObject enemyPrefab;      // Drag one of your existing enemies here
    public Transform dropPoint;         // Empty GameObject under boss where bombs spawn
    public float bombDropInterval = 2f; // How often to drop bombs
    public float enemySpawnInterval = 5f; // How often to spawn an enemy

    [Header("Health Settings")]
    public int bossHealth = 10;

    private float bombTimer;
    private float enemyTimer;

    void Start()
    {
        bombTimer = bombDropInterval;
        enemyTimer = enemySpawnInterval;
    }

    void Update()
    {
        MoveBoss();
        HandleAttacks();
    }

    void MoveBoss()
    {
        // Move left/right
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }

    void HandleAttacks()
    {
        bombTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;

        // Drop bombs
        if (bombTimer <= 0f)
        {
            DropBomb();
            bombTimer = bombDropInterval;
        }

        // Spawn enemies
        if (enemyTimer <= 0f)
        {
            SpawnEnemy();
            enemyTimer = enemySpawnInterval;
        }
    }

    void DropBomb()
    {
        if (bombPrefab != null && dropPoint != null)
        {
            Instantiate(bombPrefab, dropPoint.position, Quaternion.identity);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // Spawns enemy somewhere near the boss
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-2f, 2f), -2f, 0f);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void TakeDamage(int amount)
    {
        bossHealth -= amount;
        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Could play animation or particle effect here
        Destroy(gameObject);
    }
}


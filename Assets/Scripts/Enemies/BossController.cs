using UnityEngine;
using System;

public class BossController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 3f;
    public float maxSpeed = 5f;
    public float leftLimit = -8f;
    public float rightLimit = 8f;
    private bool movingRight = true;

    [Header("Attack Settings")]
    public GameObject bombPrefab;       // Bomb prefab
    public GameObject enemyPrefab;      // Enemy prefab

    // Two groups of drop points
    public Transform[] dropPointsGroup1;
    public Transform[] dropPointsGroup2;

    private bool useGroup1 = true; // Tracks which group is active
    public float bombDropInterval = 1.3f;
    public float enemySpawnInterval = 5f;

    [Header("Health Settings")]
    public int bossHealth = 10;

    [Header("References")]
    public GameObject player; // Assign the player here
    private float bombTimer;
    private float enemyTimer;
    private Transform[] activeGroup;

    void Start()
    {
        bombTimer = 0f; // So the first bomb drops immediately
        enemyTimer = enemySpawnInterval;

    // TEST ONLY: trigger explosions immediately
    //  if (Application.isEditor)
    //     gameObject.GetComponent<BossDeathHandler>().TriggerDeathSequence();
    }

    void Update()
    {
        MoveBoss();
        HandleAttacks();
    }

    void MoveBoss()
    {
        if (movingRight)
        {
            //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (Math.Abs(GetComponent<Rigidbody2D>().linearVelocityX) < maxSpeed)
                GetComponent<Rigidbody2D>().AddForceX(acceleration);
            
            if (transform.position.x >= rightLimit)
            {
                GetComponent<Rigidbody2D>().linearVelocityX = 0;
                movingRight = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            //transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (Math.Abs(GetComponent<Rigidbody2D>().linearVelocityX) < maxSpeed)
                GetComponent<Rigidbody2D>().AddForceX(-acceleration);
                
            if (transform.position.x <= leftLimit)
            {
                GetComponent<Rigidbody2D>().linearVelocityX = 0;
                movingRight = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    void HandleAttacks()
    {
        bombTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;

        if (bombTimer <= 0f)
        {
            bombTimer = bombDropInterval;
            DropBombs();
        }

        if (enemyTimer <= 0f)
        {
            SpawnEnemy();
            enemyTimer = enemySpawnInterval;
        }
    }

    void DropBombs()
    {
        activeGroup = useGroup1 ? dropPointsGroup1 : dropPointsGroup2;

        if (activeGroup != null && bombPrefab != null)
        {
            gameObject.GetComponent<Animator>().SetBool("attackStarted", true);
        }
    }

    // Called by the animator
    void spawnBomb()
    {
        gameObject.GetComponent<Animator>().SetBool("attackStarted", false);
        for (int i = 0; i < activeGroup.Length; i++)
        {
            Instantiate(bombPrefab, activeGroup[i].position, Quaternion.identity);
        }
        useGroup1 = !useGroup1; // Alternate groups for the next drop
        Debug.Log("Bombs dropped on " + (useGroup1 ? "Group 2" : "Group 1"));
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + UnityEngine.Random.Range(-2f, 2f),
                -2.44f, // Fixed Y position
                0f
            );

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Assign the player to the enemy's public variable
            var fields = enemy.GetComponents<MonoBehaviour>();
            foreach (var field in fields)
            {
                var playerField = field.GetType().GetField("player");
                if (playerField != null)
                {
                    playerField.SetValue(field, player);
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        bossHealth -= amount;
        if (bossHealth <= 0)
        {
            GetComponent<BossDeathHandler>()?.TriggerDeathSequence();
        }
    }


}

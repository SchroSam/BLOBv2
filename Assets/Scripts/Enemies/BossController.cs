using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
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
    public GameObject explosionPrefab; // Assign your explosion prefab here

    private float bombTimer;
    private float enemyTimer;
    private Transform globDropPoint;

    private Transform[] activeGroup;

    void Start()
    {
        bombTimer = 0f; // So the first bomb drops immediately
        enemyTimer = enemySpawnInterval;
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
            
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    void HandleAttacks()
    {
        bombTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;

        //if (bombTimer <= 0.5f)
        

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

        // foreach (Transform dropPoint in activeGroup)
        // {
            if (activeGroup != null && bombPrefab != null)
            {
                //globDropPoint = dropPoint;
                gameObject.GetComponent<Animator>().SetBool("attackStarted", true);
            }
        //}

        
        //Debug.Log("DropBomb called: " + cycles++);
    }
    
    //Called by the animator
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
                transform.position.x + Random.Range(-2f, 2f),
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
            StartCoroutine(ExplosionSequence());
        }
    }

    IEnumerator ExplosionSequence()
    {
        // Wait briefly before explosions begin
        yield return new WaitForSeconds(0.2f);

        if (explosionPrefab != null)
        {
            // Explosion 1 (left)
            Instantiate(explosionPrefab, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);

            // Explosion 2 (center)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);

            // Explosion 3 (right)
            Instantiate(explosionPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        Destroy(gameObject);
    }
}

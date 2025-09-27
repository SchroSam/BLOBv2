using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    // Trigger collider detects the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        // Spawn sprite on player
        var spawner = FindFirstObjectByType<SpawnOnPlayer>();
        if (spawner != null)
        {
            spawner.SpawnSprite();
        }

        // Destroy enemy object
        Destroy(gameObject);
    }
}

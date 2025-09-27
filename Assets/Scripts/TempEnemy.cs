using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public GameObject player;
    // Trigger collider detects the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player collided
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnSpriteOnPlayer();
            Destroy(gameObject); // Remove the enemy
        }
    }

    void SpawnSpriteOnPlayer()
    {
        var spawner = FindFirstObjectByType<SpawnOnPlayer>();
        if (spawner != null)
        {
            spawner.SpawnSprite();
        }
    }
}

using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public GameObject player;
    public int type;
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
        if (type == 0)
        {
            var spawner = FindFirstObjectByType<SpawnOnPlayer>();
            if (spawner != null)
            {
                spawner.SpawnSprite();
            }
        }
        if (type == 1)
        {
            var spawner = FindFirstObjectByType<SpawnOnPlayer>();
            if (spawner != null)
            {
                spawner.SpawnSpriteLeg();
            }
        }
    }
}

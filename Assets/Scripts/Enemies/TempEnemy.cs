using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    [Header("Char type: A = Arm, L = Leg, B = Battery")]
    public char type;
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
        if (type == 'A')
        {
            var spawner = FindFirstObjectByType<SpawnOnPlayer>();
            if (spawner != null)
            {
                spawner.SpawnSpriteArm();
            }
        }
        if (type == 'L')
        {
            var spawner = FindFirstObjectByType<SpawnOnPlayer>();
            if (spawner != null)
            {
                spawner.SpawnSpriteLeg();
            }
        }

        if (type == 'B')
            {
                var spawner = FindFirstObjectByType<SpawnOnPlayer>();
                if (spawner != null)
                {
                    spawner.SpawnSpriteBat();
                }
            }
        }
}

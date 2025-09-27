using UnityEngine;

public class SpawnOnPlayer : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject spritePrefab;   // The sprite to spawn
    public Transform player;          // Reference to the player

    public void SpawnSprite()
    {
        if (spritePrefab != null && player != null)
        {
            // Spawn at player's position, no rotation
            GameObject spawned = Instantiate(spritePrefab, player.position, Quaternion.identity);

            // Make the sprite a child of the player so it sticks
            spawned.transform.SetParent(player);
        }
    }
}

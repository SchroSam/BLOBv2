using Unity.VisualScripting;
using UnityEngine;

public class SpawnOnPlayer : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject spritePrefab;   // The sprite to spawn
    public Transform player;          // Reference to the player
    public float maxDist = 10;
    private float rval1;
    private float rval2;
    private float rval3;

    public void SpawnSprite()
    {
        rval1 = Random.Range(0, maxDist); // The distance from center
        rval2 = Random.Range(0, 359); // Rotation of the object
        rval3 = Random.Range(0, 359) * Mathf.Deg2Rad; // Rotation relative of the center

        float offsetX = Mathf.Cos(rval3) * rval1;
        float offsety = Mathf.Sin(rval3) * rval1;

        if (spritePrefab != null && player != null)
        {
            // Spawn at player's position, no rotation
            Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsety, 0);

            // Instantiate the object at the calculated position
            GameObject spawned = Instantiate(spritePrefab, spawnPosition, Quaternion.Euler(0,0,rval2));

            // Make the sprite a child of the player so it sticks
            spawned.transform.SetParent(player);
        }
    }
}

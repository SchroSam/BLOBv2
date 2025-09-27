using UnityEngine;

public class WeightPuzzel : MonoBehaviour
{
    public bool isActive = false;
    public GameObject doorLink;
    public Vector2 pos = new Vector2(0, 0);
    private Vector2 qaid;
    public int weightNeeded = 5;

    public GameObject spritePrefab;   // The sprite to spawn
    public float maxDist = 10;
    private float rval1;
    private float rval2;
    private float rval3;

    // Update is called once per frame
    private void Start()
    {
        qaid = doorLink.transform.position;
    }
    void Update()
    {
        if (weightNeeded <= 0)
        {
            isActive = true;
        }
        if (isActive)
        {
            doorLink.transform.position = Vector3.MoveTowards(doorLink.transform.position, qaid + pos, 2 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FARM"))
        {
            collision.gameObject.SetActive(false);
            weightNeeded -= 2;
            
                rval1 = Random.Range(0, maxDist); // The distance from center
                rval2 = Random.Range(0, 359); // Rotation of the object
                rval3 = Random.Range(0, 359) * Mathf.Deg2Rad; // Rotation relative of the center

                float offsetX = Mathf.Cos(rval3) * rval1;
                float offsety = Mathf.Sin(rval3) * rval1;

                // Spawn at player's position, no rotation
                Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsety, 0);

                // Instantiate the object at the calculated position
                GameObject spawned = Instantiate(spritePrefab, spawnPosition, Quaternion.Euler(0, 0, rval2));

                // Make the sprite a child of the player so it sticks
                spawned.transform.SetParent(transform);
            
        }
    }
}

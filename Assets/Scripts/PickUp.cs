using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            isCollected = true;

            // Attach to player
            transform.SetParent(collision.transform);

            // Position slightly above the player
            transform.localPosition = new Vector3(0f, 1f, 0f);

            // Make sure it renders in front of the player
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sortingOrder = 10;

            // Disable the collider so it can't trigger again
            var col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }
    }
}

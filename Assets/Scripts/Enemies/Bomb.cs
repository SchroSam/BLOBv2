using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2f;
    public int damage = 1;
    public float flashDuration = 0.15f; // short but noticeable
    public int flashCount = 3;
    public float explosionDelay = 0.2f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;   // assign in inspector or auto-find
    public Material flashMaterial;          // a white material for flashing
    public GameObject explosionEffect;      // optional

    private Material originalMaterial;
    private bool hasExploded = false;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalMaterial = spriteRenderer.material;
        else
            Debug.LogWarning("Bomb has no SpriteRenderer!", this);

        if (flashMaterial == null)
            Debug.LogWarning("Assign a white flash material in inspector!", this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasExploded)
        {
            StartCoroutine(FlashAndExplode());
        }
    }

    private IEnumerator FlashAndExplode()
    {
        hasExploded = true;

        if (spriteRenderer != null && flashMaterial != null)
        {
            for (int i = 0; i < flashCount; i++)
            {
                spriteRenderer.material = flashMaterial;  // flash white
                yield return new WaitForSeconds(flashDuration);
                spriteRenderer.material = originalMaterial; // revert
                yield return new WaitForSeconds(flashDuration);
            }
        }

        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.playerhealth -= damage;
                    player.UpdateHealthUI();
                    player.UpdatedamageOverlays();

                    if (player.playerhealth <= 0 && player.GameOverImage != null)
                        player.GameOverImage.SetActive(true);
                }
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

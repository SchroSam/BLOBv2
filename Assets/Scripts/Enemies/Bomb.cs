using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2f;
    public int damage = 1;
    public float flashDuration = 0.25f;
    public int flashCount = 3;
    public float explosionDelay = 0.2f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public GameObject explosionEffect;

    [Header("Audio")]
    public AudioClip explosionSound;           // assign explosion clip in inspector
    [Range(0f, 1f)]
    public float explosionSoundVolume = 1f;   // inspector volume control

    private bool hasExploded = false;
    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
        else
            Debug.LogWarning("Bomb has no SpriteRenderer assigned!", this);
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

        // Flash red before explosion
        if (spriteRenderer != null)
        {
            for (int i = 0; i < flashCount; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(flashDuration);

                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(flashDuration);
            }
        }

        yield return new WaitForSeconds(explosionDelay);

        Explode();
    }

    private void Explode()
    {
        // Play explosion sound at bomb position
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, explosionSoundVolume);
        }

        // Spawn explosion prefab
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Automatically destroy explosion after animation
            Animator anim = explosion.GetComponent<Animator>();
            if (anim != null)
            {
                // Wait until animation starts before getting its length
                AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
                if (clipInfo.Length > 0)
                {
                    float animLength = clipInfo[0].clip.length;
                    Destroy(explosion, animLength + 0.1f);
                }
                else
                {
                    Destroy(explosion, 1f);
                }
            }
            else
            {
                Destroy(explosion, 1f);
            }
        }

        // Damage nearby players
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

        // Destroy the bomb after the explosion
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

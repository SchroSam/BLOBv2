using UnityEngine;
using System.Collections;

public class BossDeathExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject explosionPrefab;  // The explosion animation prefab
    public Vector3[] explosionOffsets = new Vector3[]
    {
        new Vector3(-1f, 0f, 0f),
        Vector3.zero,
        new Vector3(1f, 0f, 0f)
    };
    public float delayBetweenExplosions = 0.5f;

    private bool isExploding = false;

    // Call this from the BossController when health reaches 0
    public void TriggerDeathExplosions()
    {
        if (!isExploding)
        {
            isExploding = true;
            StartCoroutine(ExplosionSequence());
        }
    }

    private IEnumerator ExplosionSequence()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < explosionOffsets.Length; i++)
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position + explosionOffsets[i], Quaternion.identity);
            }

            // Wait between explosions
            yield return new WaitForSeconds(delayBetweenExplosions);
        }

        // Destroy the boss after final explosion
        Destroy(gameObject);
    }
}

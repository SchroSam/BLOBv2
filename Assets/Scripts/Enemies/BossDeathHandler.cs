using UnityEngine;
using System.Collections;

public class BossDeathHandler : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject explosionPrefab;   // Assign your explosion prefab
    public float explosionDelay = 0.5f;  // Time between explosions

    [Header("UI Settings")]
    public GameObject sceneChangeDoor;          // "Press Enter" UI object
    private bool deathTriggered = false;

    // 👇 Call this from BossController when health reaches 0 // really, pointing down emoji?
    public void TriggerDeathSequence()
    {
        if (!deathTriggered)
        {
            deathTriggered = true;
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        // Wait briefly before explosions begin
        yield return new WaitForSeconds(0.2f);

        if (explosionPrefab != null)
        {
            // Explosion 1
            Instantiate(explosionPrefab, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(explosionDelay);

            // Explosion 2
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(explosionDelay);

            // Explosion 3
            Instantiate(explosionPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);

            Destroy(gameObject);
        }

        // Hide the boss sprite
        // SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // if (sr != null)
        //     sr.enabled = false;

        // // Show the popup
        // if (sceneChangeDoor != null)
        //     sceneChangeDoor.SetActive(true);

        // // Wait for Enter key
        // while (!Input.GetKeyDown(KeyCode.Return))
        //     yield return null;

        sceneChangeDoor.SetActive(true);

        //SceneManager.LoadScene(nextSceneName);
    }

    // IEnumerator ExplosionSequence()
    // {
    //     yield return new WaitForSeconds(0.2f);

    //     if (explosionPrefab != null)
    //     {
    //         // Explosion 1 (left)
    //         Instantiate(explosionPrefab, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
    //         yield return new WaitForSeconds(0.5f);

    //         // Explosion 2 (center)
    //         Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //         yield return new WaitForSeconds(0.5f);

    //         // Explosion 3 (right)
    //         Instantiate(explosionPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
    //         yield return new WaitForSeconds(0.3f);
    //     }

    //     Destroy(gameObject);
    // }
}

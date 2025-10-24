using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossDeathHandler : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject explosionPrefab;   // Assign your explosion prefab
    public float explosionDelay = 0.5f;  // Time between explosions

    [Header("UI Settings")]
    public GameObject endPopup;          // "Press Enter" UI object
    public string nextSceneName = "NextLevel";  // Scene to load after Enter

    private bool deathTriggered = false;

    // 👇 Call this from BossController when health reaches 0
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
        }

        // Hide the boss sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;

        // Show the popup
        if (endPopup != null)
            endPopup.SetActive(true);

        // Wait for Enter key
        while (!Input.GetKeyDown(KeyCode.Return))
            yield return null;

        SceneManager.LoadScene(nextSceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDrop : MonoBehaviour
{
    public GameObject drop;
    private void OnDestroy()
    {
        if (!Application.isPlaying || SceneManager.GetActiveScene().isLoaded == false)
            return;

        Instantiate(drop, transform.position, transform.rotation);
    }
}

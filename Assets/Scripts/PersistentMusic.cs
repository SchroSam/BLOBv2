using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //keeps it between scenes
        }
        else
        {
            Destroy(gameObject); //prevents duplicate music if scene reloads
        }
    }
}

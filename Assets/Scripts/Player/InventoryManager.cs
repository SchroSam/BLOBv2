using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton for global access

    [Header("UI")]
    public TMP_Text armsText;
    public TMP_Text legsText;
    public TMP_Text brainsText;
    public TMP_Text batteriesText;
    private PlayerController currentPlayer;
    public Color BlobColor;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between scenes

            // Listen for scene loads to re-hook UI and player
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    //right before you change scenes
    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        currentPlayer = FindFirstObjectByType<PlayerController>();
        BlobColor = currentPlayer.GetComponent<Grow>().BlobColor;
    }


    // Called automatically whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the player and UI again in the new scene
        currentPlayer = FindFirstObjectByType<PlayerController>();
        ReconnectUI();
        UpdateUIFromPlayer(currentPlayer);
        currentPlayer.GetComponent<Renderer>().material.color = BlobColor;
    }

    private void ReconnectUI()
    {
        // Try to find UI Texts again if they aren't already linked
        if (armsText == null) armsText = GameObject.Find("ArmsText")?.GetComponent<TMP_Text>();
        if (legsText == null) legsText = GameObject.Find("LegsText")?.GetComponent<TMP_Text>();
        if (brainsText == null) brainsText = GameObject.Find("BrainsText")?.GetComponent<TMP_Text>();
        if (batteriesText == null) batteriesText = GameObject.Find("BatteriesText")?.GetComponent<TMP_Text>();
    }

    public void UpdateUIFromPlayer(PlayerController player)
    {
        if (player == null) return;

        currentPlayer = player;

        if (armsText != null) armsText.text = "Arms: " + player.armCount;
        if (legsText != null) legsText.text = "Legs: " + player.legCount;
        if (batteriesText != null) batteriesText.text = "Batteries: " + player.batCount;
        if (brainsText != null) brainsText.text = "Brains: " + player.brainCount;
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null; // Singleton for global access

    [Header("UI")]
    public TMP_Text armsText;
    public TMP_Text legsText;
    public TMP_Text brainsText;
    public TMP_Text batteriesText;
    private PlayerController currentPlayer;
    public Color BlobColor;
    public List<int> cachedLimbs;
    public bool calledMyself = false;

    void Awake()
    {
        // Initialize the list
        cachedLimbs = new List<int> { 0, 0, 0, 0 };
        
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.parent.gameObject); // persists between scenes

            // Listen for scene loads to re-hook UI and player
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        else
        {
            Destroy(transform.parent.gameObject);
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

        Debug.Log($"Caching values before scene change - Arms: {cachedLimbs[0]}, Legs: {cachedLimbs[1]}, Batteries: {cachedLimbs[2]}, Brains: {cachedLimbs[3]}");
        
        BlobColor = currentPlayer.GetComponent<Grow>().BlobColor;

    }


    // Called automatically whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Cached values on scene load - Arms: {cachedLimbs[0]}, Legs: {cachedLimbs[1]}, Batteries: {cachedLimbs[2]}, Brains: {cachedLimbs[3]}");
        Debug.Log($"CalledMyself: {calledMyself}");
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

        if (calledMyself)
        {
            currentPlayer.armCount = cachedLimbs[0];
            currentPlayer.legCount = cachedLimbs[1];
            currentPlayer.batCount = cachedLimbs[2];
            currentPlayer.brainCount = cachedLimbs[3];
            calledMyself = false;
        }
        else
        {
            cachedLimbs[0] = currentPlayer.armCount;
            cachedLimbs[1] = currentPlayer.legCount;
            cachedLimbs[2] = currentPlayer.batCount;
            cachedLimbs[3] = currentPlayer.brainCount;
        }


        if (armsText != null) armsText.text = "Arms: " + currentPlayer.armCount;
        if (legsText != null) legsText.text = "Legs: " + currentPlayer.legCount;
        if (batteriesText != null) batteriesText.text = "Batteries: " + currentPlayer.batCount;
        if (brainsText != null) brainsText.text = "Brains: " + currentPlayer.brainCount;
    }
}

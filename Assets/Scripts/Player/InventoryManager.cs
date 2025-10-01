using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton for easy access

    [Header("UI")]
    public TMP_Text armsText;
    public TMP_Text legsText;
    public TMP_Text brainsText;
    public TMP_Text batteriesText;



    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Call this method from your player script to update the UI with current counts.
    /// </summary>
    /// <param name="player">The player with the Experiment script</param>
    public void UpdateUIFromPlayer(PlayerController player)
    {
        if (player == null) return;

        if (armsText != null) armsText.text = "Arms: " + player.armCount;
        if (legsText != null) legsText.text = "Legs: " + player.legCount;
        if (batteriesText != null) batteriesText.text = "Batteries: " + player.batCount;
        if (brainsText != null) brainsText.text = "Brains: " + player.brainCount; // brains can be separate
    }
}

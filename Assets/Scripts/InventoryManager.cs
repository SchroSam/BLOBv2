using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton for easy access

    [Header("Counts")]
    public int arms;
    public int legs;
    public int brains;
    public int batteries; // Added separate batteries count

    [Header("UI")]
    public TMP_Text armsText;
    public TMP_Text legsText;
    public TMP_Text brainsText;
    public TMP_Text batteriesText; // Added separate UI for batteries

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // Add methods
    public void AddArms(int amount)
    {
        arms += amount;
        UpdateUI();
    }

    public void AddLegs(int amount)
    {
        legs += amount;
        UpdateUI();
    }

    public void AddBrains(int amount)
    {
        brains += amount;
        UpdateUI();
    }

    public void AddBatteries(int amount)
    {
        batteries += amount;
        UpdateUI();
    }

    // Use methods
    public bool UseArms(int amount)
    {
        if (arms >= amount)
        {
            arms -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool UseLegs(int amount)
    {
        if (legs >= amount)
        {
            legs -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool UseBrains(int amount)
    {
        if (brains >= amount)
        {
            brains -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool UseBatteries(int amount)
    {
        if (batteries >= amount)
        {
            batteries -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    // Update all UI elements
    void UpdateUI()
    {
        if (armsText != null) armsText.text = "Arms: " + arms;
        if (legsText != null) legsText.text = "Legs: " + legs;
        if (brainsText != null) brainsText.text = "Brains: " + brains;
        if (batteriesText != null) batteriesText.text = "Batteries: " + batteries;
    }
}

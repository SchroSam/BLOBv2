using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton for easy access

    [Header("Counts")]
    public int arms;
    public int legs;
    public int brains;

    [Header("UI")]
    public TMP_Text armsText;
    public TMP_Text legsText;
    public TMP_Text brainsText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

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

    void UpdateUI()
    {
        armsText.text = "Arms: " + arms;
        legsText.text = "Legs: " + legs;
        brainsText.text = "Brains: " + brains;
    }
}

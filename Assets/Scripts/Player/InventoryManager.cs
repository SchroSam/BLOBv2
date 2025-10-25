using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.U2D.IK;

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
    public bool doRetrieveCache = false;
    public List<LimbData> cachedArmFloaters;
    public List<LimbData> cachedLegFloaters;
    public List<LimbData> cachedBatteryFloaters;

    // Static backup in case the singleton is recreated during scene transition
    private static List<int> s_cachedLimbs;
    private static List<LimbData> s_cachedArmFloaters;
    private static List<LimbData> s_cachedLegFloaters;
    private static List<LimbData> s_cachedBatteryFloaters;
    private static bool s_doRetrieveCache;

    void Awake()
    {
        // Initialize the list
        cachedLimbs = new List<int> { 0, 0, 0, 0 };
        cachedArmFloaters = new List<LimbData>();
        cachedLegFloaters = new List<LimbData>();
        cachedBatteryFloaters = new List<LimbData>();

        Debug.Log($"InventoryManager.Awake this={GetInstanceID()} name={gameObject.name} InstanceExists={(Instance!=null)} hasStaticBackup={s_doRetrieveCache}");

        // If we have a static backup (from previous instance), restore it
        if (s_doRetrieveCache)
        {
            cachedLimbs = new List<int>(s_cachedLimbs);
            cachedArmFloaters = new List<LimbData>(s_cachedArmFloaters);
            cachedLegFloaters = new List<LimbData>(s_cachedLegFloaters);
            cachedBatteryFloaters = new List<LimbData>(s_cachedBatteryFloaters);
            doRetrieveCache = true;
            Debug.Log($"Restored from static backup: arms={cachedArmFloaters.Count} legs={cachedLegFloaters.Count} bat={cachedBatteryFloaters.Count}");
        }
        
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between scenes

            // Listen for scene loads to re-hook UI and player
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            Debug.Log($"InventoryManager set as Instance this={GetInstanceID()} name={gameObject.name}");
        }
        else
        {
            Debug.Log($"InventoryManager duplicate found, destroying this={GetInstanceID()} name={gameObject.name} existingInstance={Instance.GetInstanceID()}\n");
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        Debug.Log($"InventoryManager.OnDestroy this={GetInstanceID()} name={gameObject.name}");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    // Called by SpawnOnPlayer to atomically cache LimbData before a scene change
    public void CacheLimbData(List<LimbData> armsData, List<LimbData> legsData, List<LimbData> batData)
    {
        // Cache in both instance and static backup
        cachedArmFloaters = new List<LimbData>(armsData ?? new List<LimbData>());
        cachedLegFloaters = new List<LimbData>(legsData ?? new List<LimbData>());
        cachedBatteryFloaters = new List<LimbData>(batData ?? new List<LimbData>());
        doRetrieveCache = true;

        // Static backup
        s_cachedLimbs = new List<int>(cachedLimbs);
        s_cachedArmFloaters = new List<LimbData>(cachedArmFloaters);
        s_cachedLegFloaters = new List<LimbData>(cachedLegFloaters);
        s_cachedBatteryFloaters = new List<LimbData>(cachedBatteryFloaters);
        s_doRetrieveCache = true;

        Debug.Log($"CacheLimbData called: arms={cachedArmFloaters.Count} legs={cachedLegFloaters.Count} bat={cachedBatteryFloaters.Count} doRetrieveCache={doRetrieveCache} (backed up to static)");
    }

    public void clearLimbInfo()
    {
        cachedArmFloaters.Clear();
        cachedArmFloaters.Clear();
        cachedArmFloaters.Clear();

        s_cachedLimbs.Clear();
        s_cachedArmFloaters.Clear();
        s_cachedLegFloaters.Clear();
        s_cachedBatteryFloaters.Clear();

        doRetrieveCache = false;
        s_doRetrieveCache = false;
    }

    //right before you change scenes
    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        currentPlayer = FindFirstObjectByType<PlayerController>();

        //Debug.Log($"Caching values before scene change - Arms: {cachedLimbs[0]}, Legs: {cachedLimbs[1]}, Batteries: {cachedLimbs[2]}, Brains: {cachedLimbs[3]}");
        
        BlobColor = currentPlayer.GetComponent<Grow>().BlobColor;

    }


    // Called automatically whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded: doRetrieveCache={doRetrieveCache} cachedArmFloaters={cachedArmFloaters?.Count ?? 0} cachedLegFloaters={cachedLegFloaters?.Count ?? 0} cachedBatteryFloaters={cachedBatteryFloaters?.Count ?? 0}");
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

        Debug.Log($"UpdateUIFromPlayer: doRetrieveCache={doRetrieveCache} cachedArmFloaters={cachedArmFloaters?.Count ?? 0} cachedLegFloaters={cachedLegFloaters?.Count ?? 0} cachedBatteryFloaters={cachedBatteryFloaters?.Count ?? 0}");

        if (doRetrieveCache)
        {
            // Restore numeric counts
            currentPlayer.armCount = cachedLimbs[0];
            currentPlayer.legCount = cachedLimbs[1];
            currentPlayer.batCount = cachedLimbs[2];
            currentPlayer.brainCount = cachedLimbs[3];

            // Recreate limb GameObjects from cached LimbData
            var spawnComp = currentPlayer.GetComponent<SpawnOnPlayer>();
            if (spawnComp != null)
            {
                spawnComp.arms = new System.Collections.Generic.List<GameObject>();
                spawnComp.legs = new System.Collections.Generic.List<GameObject>();
                spawnComp.bat = new System.Collections.Generic.List<GameObject>();

                foreach (var ld in cachedArmFloaters)
                {
                    if (ld.prefab == null) continue;
                    Debug.Log(ld.prefab.name);
                    GameObject spawned = Instantiate(ld.prefab, player.transform.GetChild(0));
                    spawned.transform.localPosition = ld.localPosition;
                    spawned.transform.localRotation = ld.localRotation;
                    spawned.transform.localScale = ld.localScale;
                    spawnComp.arms.Add(spawned);
                }

                foreach (var ld in cachedLegFloaters)
                {
                    if (ld.prefab == null) continue;
                    Debug.Log(ld.prefab.name);
                    GameObject spawned = Instantiate(ld.prefab, player.transform.GetChild(0));
                    spawned.transform.localPosition = ld.localPosition;
                    spawned.transform.localRotation = ld.localRotation;
                    spawned.transform.localScale = ld.localScale;
                    spawnComp.legs.Add(spawned);
                }

                foreach (var ld in cachedBatteryFloaters)
                {
                    if (ld.prefab == null) continue;
                    Debug.Log(ld.prefab.name);
                    GameObject spawned = Instantiate(ld.prefab, player.transform.GetChild(0));
                    spawned.transform.localPosition = ld.localPosition;
                    spawned.transform.localRotation = ld.localRotation;
                    spawned.transform.localScale = ld.localScale;
                    spawnComp.bat.Add(spawned);
                }
            }

            doRetrieveCache = false;
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

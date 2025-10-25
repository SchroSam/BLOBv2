using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnOnPlayer : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject spritePrefabArm;   // The sprite to spawn
    public GameObject spritePrefabLeg;
    public GameObject spritePrefabBat;
    public Transform player;          // Reference to the player
    public float maxDist = 10;
    private float rval1;
    private float rval2;
    private float rval3;
    public List<GameObject> arms;
    public List<GameObject> legs;
    public List<GameObject> brain;
    public List<GameObject> bat;
    // Structured data for persistence across scenes
    public List<LimbData> armsData;
    public List<LimbData> legsData;
    public List<LimbData> batData;


    void Awake()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        armsData = new List<LimbData>();
        legsData = new List<LimbData>();
        batData = new List<LimbData>();
    }
    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    public void SpawnSpriteArm()
    {
        rval1 = Random.Range(0, maxDist); // The distance from center
        rval2 = Random.Range(0, 359); // Rotation of the object
        rval3 = Random.Range(0, 359) * Mathf.Deg2Rad; // Rotation relative of the center

        float offsetX = Mathf.Cos(rval3) * rval1;
        float offsety = Mathf.Sin(rval3) * rval1;

        if (spritePrefabArm != null && player != null)
        {
            // Spawn at player's position, no rotation
            Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsety, 0);

            // Instantiate the object at the calculated position
            GameObject spawned = Instantiate(spritePrefabArm, spawnPosition, Quaternion.Euler(0, 0, rval2));

            // Make the sprite a child of the player so it sticks
            spawned.transform.SetParent(player);

            arms.Add(spawned);
            // record structured data for this spawned limb
            LimbData ld = new LimbData { prefab = spritePrefabArm, localPosition = spawned.transform.localPosition, localRotation = spawned.transform.localRotation, localScale = spawned.transform.localScale };
            armsData.Add(ld);
            Debug.Log($"Spawned arm prefab={ld.prefab?.name} localPos={ld.localPosition} armsDataCount={armsData.Count}");
        }
    }
    public void SpawnSpriteLeg()
    {
        rval1 = Random.Range(0, maxDist); // The distance from center
        rval2 = Random.Range(0, 359); // Rotation of the object
        rval3 = Random.Range(0, 359) * Mathf.Deg2Rad; // Rotation relative of the center

        float offsetX = Mathf.Cos(rval3) * rval1;
        float offsety = Mathf.Sin(rval3) * rval1;

        if (spritePrefabLeg != null && player != null)
        {
            // Spawn at player's position, no rotation
            Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsety, 0);

            // Instantiate the object at the calculated position
            GameObject spawned = Instantiate(spritePrefabLeg, spawnPosition, Quaternion.Euler(0, 0, rval2));

            // Make the sprite a child of the player so it sticks
            spawned.transform.SetParent(player);

            legs.Add(spawned);
            LimbData ld = new LimbData { prefab = spritePrefabLeg, localPosition = spawned.transform.localPosition, localRotation = spawned.transform.localRotation, localScale = spawned.transform.localScale };
            legsData.Add(ld);
            Debug.Log($"Spawned leg prefab={ld.prefab?.name} localPos={ld.localPosition} legsDataCount={legsData.Count}");
        }
    }
    public void SpawnSpriteBat()
    {
        rval1 = Random.Range(0, maxDist); // The distance from center
        rval2 = Random.Range(0, 359); // Rotation of the object
        rval3 = Random.Range(0, 359) * Mathf.Deg2Rad; // Rotation relative of the center

        float offsetX = Mathf.Cos(rval3) * rval1;
        float offsety = Mathf.Sin(rval3) * rval1;

        if (spritePrefabBat != null && player != null)
        {
            // Spawn at player's position, no rotation
            Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsety, 0);

            // Instantiate the object at the calculated position
            GameObject spawned = Instantiate(spritePrefabBat, spawnPosition, Quaternion.Euler(0, 0, rval2));

            // Make the sprite a child of the player so it sticks
            spawned.transform.SetParent(player);

            bat.Add(spawned);
            LimbData ld = new LimbData { prefab = spritePrefabBat, localPosition = spawned.transform.localPosition, localRotation = spawned.transform.localRotation, localScale = spawned.transform.localScale };
            batData.Add(ld);
            Debug.Log($"Spawned bat prefab={ld.prefab?.name} localPos={ld.localPosition} batDataCount={batData.Count}");
        }
    }
    public void KillLimbArm()
    {
        // ranob = Random.Range(0, arms.Count - 1);
        // myObject = arms[ranob];
        if (arms.Count > 0) arms.RemoveAt(0);
        if (armsData != null && armsData.Count > 0) armsData.RemoveAt(0);
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            if (gameObject.transform.GetChild(0).GetChild(i).tag == "Arm")
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
                break;
            }
        }
    }
    public void KillLimbLeg()
    {
        // ranob = Random.Range(0, legs.Count - 1);
        // myObject = legs[ranob];
        if (legs.Count > 0) legs.RemoveAt(0);
        if (legsData != null && legsData.Count > 0) legsData.RemoveAt(0);
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            if (gameObject.transform.GetChild(0).GetChild(i).tag == "Leg")
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
                break;
            }
        }
    }
    public void KillLimbBrain()
    {
        // ranob = Random.Range(0, brain.Count - 1);
        // myObject = brain[ranob];
        brain.RemoveAt(0);
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            if (gameObject.transform.GetChild(0).GetChild(i).tag == "Brain")
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
                break;
            }
        }
    }
    public void KillLimbBat()
    {
        // ranob = Random.Range(0, bat.Count - 1);
        // myObject = bat[ranob];
        if (bat.Count > 0) bat.RemoveAt(0);
        if (batData != null && batData.Count > 0) batData.RemoveAt(0);
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            if (gameObject.transform.GetChild(0).GetChild(i).tag == "Battery")
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
                break;
            }
        }
    }

    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        if(newScene.name != "MainMenu")
        {
            Debug.Log(newScene.name);
            // Copy current spawned lists to the persistent inventory manager.
            // Note: these are references to scene GameObjects and the objects
            // themselves will be destroyed when the scene unloads. See notes
            // in InventoryManager about storing prefab references instead.
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.CacheLimbData(armsData, legsData, batData);
                Debug.Log($"Copied LimbData to InventoryManager via CacheLimbData: arms={armsData.Count}, legs={legsData.Count}, bat={batData.Count}");
            }
        }
    }
}

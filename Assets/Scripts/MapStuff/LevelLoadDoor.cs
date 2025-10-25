using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LevelLoadDoor : MonoBehaviour
{
    public string sceneToLoad;
    private bool inputsActive = false;
    private bool isLoading = false;

    public GameObject fadeObject;


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            inputsActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            inputsActive = false;
        }
    }

    void Update()
    {
        if (inputsActive && Input.GetKeyDown(KeyCode.Return) && !isLoading)
        {
            //Debug.Log("main if entered");
            PlayerController player = FindFirstObjectByType<PlayerController>();
            var spawnComp = player.GetComponent<SpawnOnPlayer>();
            
            // Cache both limb counts and LimbData in one atomic operation
            var limbs = new List<int> { player.armCount, player.legCount, player.batCount, player.brainCount };
            InventoryManager.Instance.cachedLimbs = limbs;
            InventoryManager.Instance.CacheLimbData(spawnComp.armsData, spawnComp.legsData, spawnComp.batData);
            
            Debug.Log($"LevelLoadDoor caching before scene load - Counts: {string.Join(",", limbs)}, Data: arms={spawnComp.armsData?.Count ?? 0} legs={spawnComp.legsData?.Count ?? 0} bat={spawnComp.batData?.Count ?? 0}");

            isLoading = true;
            if (fadeObject != null) //wait for transition
                StartCoroutine(PauseThenTransition());
            else if (fadeObject == null)
            {
                //Debug.Log("scene erroneously loaded");
                SceneManager.LoadScene(sceneToLoad);
            }

        }
    }

    IEnumerator PauseThenTransition()
    {
        // Wait briefly before explosions begin
        fadeObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);

    }

}

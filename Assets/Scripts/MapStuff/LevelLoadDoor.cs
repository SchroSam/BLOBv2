using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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
            InventoryManager.Instance.cachedLimbs[0] = FindFirstObjectByType<PlayerController>().armCount;
            InventoryManager.Instance.cachedLimbs[1] = FindFirstObjectByType<PlayerController>().legCount;
            InventoryManager.Instance.cachedLimbs[2] = FindFirstObjectByType<PlayerController>().batCount;
            InventoryManager.Instance.cachedLimbs[3] = FindFirstObjectByType<PlayerController>().brainCount;
            InventoryManager.Instance.calledMyself = true;

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

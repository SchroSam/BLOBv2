using UnityEngine;
using UnityEngine.SceneManagement;

public class JanitorDoor : MonoBehaviour
{

    private bool inputsActive = false;
    private bool isLoading = false;
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
            SceneManager.LoadScene("JanitorBossRoom");
            isLoading = true;
        }
    }

}

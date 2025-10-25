using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject QuitMenu;
    public GameObject CredMenu;

    [Header("Controls Display")]
    public GameObject controlsImage;   // Assign your controls image here
    public float controlsDisplayTime = 7f; // Time to show controls

    public void TitileNowButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayNowButton()
    {
        StartCoroutine(ShowControlsAndStartGame());
    }

   private IEnumerator ShowControlsAndStartGame()
    {
        if (controlsImage != null)
         controlsImage.SetActive(true);

        yield return new WaitForSeconds(controlsDisplayTime);

         
            

    // Smoothly load the main game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {   
        yield return null; // wait until scene is fully loaded
        }
    }

    public void CredNowButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}



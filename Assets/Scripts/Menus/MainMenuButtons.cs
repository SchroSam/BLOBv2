using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject QuitMenu;
    public GameObject CredMenu;
    // Start is called before the first frame update
    public void TitileNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void CredNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }

}


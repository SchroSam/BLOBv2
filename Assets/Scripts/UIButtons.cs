using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject QuitMenu;
    // Start is called before the first frame update
    public void TitileNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("BlobPhysTest");
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("THEGAME");
    }
    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}


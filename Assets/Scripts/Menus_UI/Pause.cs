using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1.0f;
    }
    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void Quite()
    {
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuBehavior : MonoBehaviour
{
    public TextMeshProUGUI TimePlayedText;
    public GameObject pauseMenu;

    float timer;
    public static bool isGamePaused = false;
    private void Start()
    {
        timer = PlayerPrefs.GetFloat("TimePlayed");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        TimePlayedText.text = "Time Played: " + (int)timer + "s";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // if trying to pause during a level up, nothing happened
        if (!GameStateManager.isPaused)
        {
            isGamePaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        // if trying to or resume during a level up, nothing happened
        if (!GameStateManager.isPaused)
        {
            isGamePaused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveTimer()
    {
        PlayerPrefs.SetFloat("TimePlayed", timer);
    }
}

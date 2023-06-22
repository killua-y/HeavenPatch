using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI TimePlayedText;

    float timer;

    private void Start()
    {
        timer = PlayerPrefs.GetFloat("TimePlayed");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        TimePlayedText.text = "Time Played: " + (int)timer + "s";
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("TimePlayed", timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGameWithUI()
    {

    }
}

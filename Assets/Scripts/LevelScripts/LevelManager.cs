using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;

    public string nextLevel;

    public Text gameText;

    public AudioClip gameOverSFX;

    public AudioClip winSFX;

    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "Game Over!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "You Win!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        Invoke("LoadNextLevel", 2);

        // save the time played
        PauseMenu.GetComponent<PauseMenuBehavior>().SaveTimer();
    }
    
    void LoadNextLevel(){
        //Debug.Log("AAAAA");
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

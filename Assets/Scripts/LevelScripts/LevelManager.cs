using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;

    public string nextLevel;
    

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    void LoadNextLevel(){
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

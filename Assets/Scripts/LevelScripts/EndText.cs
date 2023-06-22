using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndText : MonoBehaviour
{
    public GameObject nextPage;  // Reference to the next page GameObject

    public Text pageText;  // Reference to the Text component
    public Button nextButton;  // Reference to the Button component
    public Text buttonText; // Reference to the Button's Text component

    private int currentPageIndex = 0;  // Variable to track the current page index
    private string[] pageTexts = new string[]{
    "With the aid of Shun, Nuwa has eventually built the patch using the divine stones and fixed the Heaven...",

    "The world is once again settled in peaceness and harmony, and Shun eventually become the ruler of the land he had protected",
    "..."};

    
    private void Start()
    {
        // Set the initial page text
        pageText.text = pageTexts[currentPageIndex];

        // Add a listener to the nextButton
        nextButton.onClick.AddListener(NextPage);
    }

    private void NextPage()
    {
        // Increase the currentPageIndex
        currentPageIndex++;

        // Check if the currentPageIndex is within bounds
        if (currentPageIndex < pageTexts.Length - 1)
        {
            // Set the text for the next page
            pageText.text = pageTexts[currentPageIndex];
            buttonText.text = "Replay Game";
        }
        else
        {
            // If there are no more pages, deactivate the current page and activate the nextPage
            gameObject.SetActive(false);
            nextPage.SetActive(true);
            
            SceneManager.LoadScene(0);
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene based on the build index of the current scene + 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // Check if the next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there are no more scenes, display a message or perform other actions
            Debug.Log("No more scenes to load.");
        }
    }
}



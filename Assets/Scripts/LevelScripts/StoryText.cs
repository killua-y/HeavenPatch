using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StoryText : MonoBehaviour
{
    public GameObject nextPage;  // Reference to the next page GameObject

    public Text pageText;  // Reference to the Text component
    public Button nextButton;  // Reference to the Button component
    public Text buttonText; // Reference to the Button's Text component

    private int currentPageIndex = 0;  // Variable to track the current page index
    private string[] pageTexts = new string[]{
    "Due to a war between Zhurong, the God of Fire, and Gonggong, the God of Water, the portions of the sky sustained by these two gods became critically damaged, leaving the vault of heaven teetering on the brink of collapse. This celestial turmoil causes disasters on earth. Raging fires and floods ravage the land, and fierce creatures emerge from the depths of the earth. Nuwa, the Mother God, is responsible for being the last holder of the vault of heaven. As a last resort, she awakens you, Shun, to undertake an epic quest to collect the divine stones capable of repairing the heavens and to convince the absent gods to return and resume their roles.",

    "Controls: Use W A S D to control movement, Use Mouse to control targeting direction, Use Space for Dash. Skills and Weapons are automatic",
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
                        buttonText.text = "Start Game";
        }
        else
        {
            // If there are no more pages, deactivate the current page and activate the nextPage
            gameObject.SetActive(false);
            nextPage.SetActive(true);
            LoadNextScene();
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



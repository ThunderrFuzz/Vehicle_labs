using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public Text endGameText;
    public GMPoints gmPoints;

    void Start()
    {
        // Find the Text component if not assigned in the inspector
        
        
         endGameText = GetComponent<Text>();
        

        // Hide the end game text initially
        endGameText.enabled = false;

        // Find the GMPoints script
        gmPoints = FindObjectOfType<GMPoints>();
    }

    // Call this method to display the end game message
    public void ShowEndGameMessage()
    {
        // Enable the Text component and set the end game message
        endGameText.enabled = true;
        endGameText.text = "Total Points: " + gmPoints.totalScore + "\n Press R T" ;
    }

    // Call this method to hide the end game message
    public void HideEndGameMessage()
    {
        // Disable the Text component
        endGameText.enabled = false;
    }
}


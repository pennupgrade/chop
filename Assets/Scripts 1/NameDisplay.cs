using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameDisplay : MonoBehaviour
{
    public TMP_Text greetingText;

    private void Start()
    {
        // Find the PlayerData script
        PlayerData playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        // Check if PlayerData exists and has a valid name
        if (playerData != null && !string.IsNullOrEmpty(playerData.playerName))
        {
            // Display a greeting with the player's name
            greetingText.text = "Hi " + playerData.playerName + "! Choose your clothing for the day below :)";
        }
        else
        {
            // If the player's name is not available, display a default greeting
            greetingText.text = "Hi there! Choose your clothing for the day below :)";
        }
    }
}


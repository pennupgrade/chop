using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameDisplayChop : MonoBehaviour
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
            greetingText.text = "Nice to meet you " + playerData.playerName + "! We will be putting stickers on you to check how you sleep. The stickers won’t hurt you.";
        }
        else
        {
            // If the player's name is not available, display a default greeting
            greetingText.text = "Nice to meet you! We will be putting stickers on you to check how you sleep. The stickers won’t hurt you.";
        }
    }
}


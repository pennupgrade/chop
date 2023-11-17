using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInput : MonoBehaviour
{

    public void SaveName(TMP_InputField nameInputField)
    {
        // Retrieve the name from the input field
        string playerName = nameInputField.text;

        // Store the name in the PlayerData script
        PlayerData pd = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        pd.playerName = playerName;

        Debug.Log(playerName);


        // Load the next scene
        // Example: 
        //SceneManager.LoadScene("ChooseClothing");
    }
}

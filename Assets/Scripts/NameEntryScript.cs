using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameEntryScript : MonoBehaviour
{
    public TMP_InputField nameField;
    public GameObject storage;
    public void OnNameEntered() {
        storage.name = nameField.text;
    }
}

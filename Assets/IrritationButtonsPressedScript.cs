using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IrritationButtonsPressedScript : MonoBehaviour
{
    public RawImage playerChoice;
    public Texture2D[] choices;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(int button)
    {
        Debug.Log("Button " + button + " pressed");
        playerChoice.texture = choices[button - 1];
        playerChoice.gameObject.SetActive(true);
    }
}

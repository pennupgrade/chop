using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IrritationButtonsPressedScript : MonoBehaviour
{
    public RawImage playerChoice;
    public Texture2D[] choices;

    float timer;
    bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressed)
            return;
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer <= 0)
            SceneManager.LoadScene(10);
    }

    public void OnButtonPressed(int button)
    {
        Debug.Log("Button " + button + " pressed");
        playerChoice.texture = choices[button - 1];
        playerChoice.gameObject.SetActive(true);
        timer = 10;
        pressed = true;
    }
}

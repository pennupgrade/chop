using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepingManager : MonoBehaviour
{
    public Button StartButton;
    public Button LeftFlip;
    public Button RightFlip;

    public GameObject UI;
    private GameObject Frame;
    private bool StartGame;

    public RawImage player;
    public Texture[] playerTextures;

    public TMPro.TextMeshProUGUI TitleLabel;
    public TMPro.TextMeshProUGUI TimeLabel;
    public TMPro.TextMeshProUGUI ButtonLabel;
    public GameObject Timebar;

    private float GameTimer = 20.0f;
    private Vector3 InitialSize = new Vector3(0.0f, 0.0f, 0.0f);
    
    private IEnumerator coroutine;
    private bool IsFlipped = false;

    // Left = false, Right = true
    private bool Side = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Frame = UI.transform.Find("Frame").gameObject;
        InitialSize = Timebar.transform.localScale;
        StartButton.onClick.AddListener(GoOnClick);

        LeftFlip.onClick.AddListener(() => FlipPerson(false));
        RightFlip.onClick.AddListener(() => FlipPerson(true));
    }

    private IEnumerator TurnEvent() {
        float initialtime = 4.0f;
        float t = initialtime;

        TimeLabel.text = "Flip them " + (Side ? "right" : "left") + "!";

        while(t > 0.0f && IsFlipped) {
            float ratio = t / initialtime;
            float width = Mathf.Lerp(1.0f, InitialSize.x, ratio);
            Timebar.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.green, ratio);
            Timebar.transform.localScale = new Vector3(width, InitialSize.y, InitialSize.z);

            t -= Time.deltaTime;
            yield return null;
        }

        if(t <= 0.0f) {
            Debug.Log("Task failed!");
            TimeLabel.text = "Oh no!";
            player.texture = playerTextures[2];
            StartGame = false;

            Frame.SetActive(true);
            ButtonLabel.text = "Try again?";
            TitleLabel.text = "Woops! Don't sleep on your belly!";

            yield return null;
        } else {
            Timebar.GetComponent<SpriteRenderer>().color = Color.white;
            TimeLabel.text = "Good job!";
            player.texture = playerTextures[0];
            
            Vector3 newSize = player.transform.localScale;
            newSize.x = Mathf.Abs(newSize.x);
            player.transform.localScale = newSize;

            IsFlipped = false;
        }

        float currWidth = Timebar.transform.localScale.x;
        Color currColor = Timebar.GetComponent<SpriteRenderer>().color;
        
        // set t to the ratio, should be 0.0f to 1.0f
        t = t / initialtime;
        initialtime = 0.5f;
        // t is now from 0.0f to 0.5f
        t = t * initialtime;

        while(t > 0.0f) {
            float ratio = 1.0f - t / initialtime;
            float width = Mathf.Lerp(currWidth, InitialSize.x, ratio);
            
            Timebar.GetComponent<SpriteRenderer>().color = Color.Lerp(currColor, Color.white, ratio);
            Timebar.transform.localScale = new Vector3(width, InitialSize.y, InitialSize.z);

            t -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("TurnEvent end, rest");
        //player.GetComponent<SpriteRenderer>().color = Color.white;
        Timebar.transform.localScale = InitialSize;
        yield return new WaitForSeconds(5.0f);

        coroutine = null;
    }
    
    void FlipPerson(bool direction) {
        // reverse sort of logic if they're facing right, we want to flip it lefts
        if (direction == !Side) {
            IsFlipped = false;
            Debug.Log("Correct!");
        }
    }

    void ResetGame()
    {
        GameTimer = 20.0f;
        TimeLabel.text = "";
        Timebar.transform.localScale = InitialSize;
        Timebar.GetComponent<SpriteRenderer>().color = Color.white;
        player.texture = playerTextures[0];
        IsFlipped = false;
    }

    void GoOnClick()
    {
        Frame.SetActive(false);
        StartGame = true;
        ResetGame();
        Debug.Log("Hit it");
    }

    void GameLoop()
    {
        if (GameTimer > 0.0f) {
            GameTimer -= Time.deltaTime;

            float random = Random.Range(0.0f, 1.0f);

            if (!IsFlipped && coroutine == null) {
                Side = random > 0.5f;
                IsFlipped = true;

                player.texture = playerTextures[1];

                Vector3 playerSize = player.transform.localScale;
                playerSize.x = Side ? 1.0f * playerSize.x : -1.0f * playerSize.x;
                player.transform.localScale = playerSize;

                coroutine = TurnEvent();
                StartCoroutine(coroutine);
            }

        } else {
            Debug.Log("Game ended, you win!");
            StartGame = false;
        }
    }

    void Update()
    {
        if (StartGame) {
            GameLoop();
        }
    }
}

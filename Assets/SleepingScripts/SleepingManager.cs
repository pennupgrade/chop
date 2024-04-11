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

    private float GameTimer = 60.0f;
    private Vector3 InitialSize = new Vector3(0.0f, 0.0f, 0.0f);
    
    private IEnumerator coroutine;
    private bool IsFlipped = false;

    public GameObject[] stickers;

    // Left = false, Right = true
    private bool Side = false;
    private bool Orientation = false;

    void BlockStickerUI() {
        foreach (GameObject SleepingSticker in stickers) {
            SleepingSticker.SetActive(false);
        }
    }

    void ShowStickerUI() {
        foreach (GameObject SleepingSticker in stickers) {
            SleepingSticker.SetActive(true);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Frame = UI.transform.Find("Frame").gameObject;
        InitialSize = Timebar.transform.localScale;
        StartButton.onClick.AddListener(GoOnClick);

        LeftFlip.onClick.AddListener(() => FlipPerson(false));
        RightFlip.onClick.AddListener(() => FlipPerson(true));

        // for the love of me i can't figure out why the UI doesn't want to hide this
        BlockStickerUI();
    }

    void flipTexture() {
        Vector3 playerScale = player.transform.localScale;
        playerScale.x *= -1.0f;
        player.transform.localScale = playerScale;
    }

    void resetOrientation() {
        Vector3 playerScale = player.transform.localScale;
        playerScale.x = Mathf.Abs(playerScale.x);
        player.transform.localScale = playerScale;
    }

    void adjutstTimebar(float t, float initialtime) {
        float ratio = t / initialtime;
        float width = Mathf.Lerp(1.0f, InitialSize.x, ratio);
        Timebar.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.green, ratio);
        Timebar.transform.localScale = new Vector3(width, InitialSize.y, InitialSize.z);
    }

    void resetTimebar(float t, float initialtime, float currWidth, Color currColor) {
        float ratio = 1.0f - t / initialtime;
        float width = Mathf.Lerp(currWidth, InitialSize.x, ratio);
        
        Timebar.GetComponent<SpriteRenderer>().color = Color.Lerp(currColor, Color.white, ratio);
        Timebar.transform.localScale = new Vector3(width, InitialSize.y, InitialSize.z);
    }

    private IEnumerator LooseBandageEvent() {
        bool finished = false;
        float initialtime = 10.0f;
        float t = initialtime;

        TimeLabel.text = "Your parent must drag the equipment back on!";
        // face up
        player.texture = playerTextures[0];
        ShowStickerUI();
        
        yield return new WaitForSeconds(2.5f);

        foreach (GameObject SleepingSticker in stickers) {
            GameObject sticker = SleepingSticker.transform.Find("sticker").gameObject;
            sticker.GetComponent<SleepDragDrop>().SetActiveState(true);
        }

        while (t > 0.0f && !finished) {
            bool allFinished = true;

            foreach (GameObject SleepingSticker in stickers) {
                GameObject sticker = SleepingSticker.transform.Find("sticker").gameObject;
                if (!sticker.GetComponent<SleepDragDrop>().IsFinished()) {
                    allFinished = false;

                    if (!sticker.GetComponent<SleepDragDrop>().IsMoving()) {
                        Vector3 moveDirection = sticker.GetComponent<SleepDragDrop>().GetMoveDirection();
                        float speed = 0.1f;
                        sticker.transform.localPosition += moveDirection * Time.deltaTime * speed;
                    }
                }
            }

            if (allFinished) {
                finished = true;
            }

            adjutstTimebar(t, initialtime);
            t -= Time.deltaTime;
            yield return null;
        }

        if(t <= 0.0f) {
            Debug.Log("Task failed!");
            TimeLabel.text = "Oh no!";
            StartGame = false;
            BlockStickerUI();

            Frame.SetActive(true);
            ButtonLabel.text = "Try again?";
            TitleLabel.text = "Woops! Devices must be secure.";

            yield return null;
        } else {
            Timebar.GetComponent<SpriteRenderer>().color = Color.white;
            TimeLabel.text = "Good job!";
        }
        
        float currWidth = Timebar.transform.localScale.x;
        Color currColor = Timebar.GetComponent<SpriteRenderer>().color;
        
        t = t / initialtime;
        initialtime = 0.5f;
        t = t * initialtime;

        while(t > 0.0f) {
            resetTimebar(t, initialtime, currWidth, currColor);
            t -= Time.deltaTime;
            yield return null;
        }

        Timebar.transform.localScale = InitialSize;

        foreach (GameObject SleepingSticker in stickers) {
            GameObject sticker = SleepingSticker.transform.Find("sticker").gameObject;
            sticker.GetComponent<SleepDragDrop>().reset();
        }

        yield return new WaitForSeconds(1.0f);

        coroutine = null;
    }

    private IEnumerator TurnEvent() {
        float initialtime = 4.0f;
        float t = initialtime;

        // face up
        player.texture = playerTextures[0];
        ShowStickerUI();

        // flip the player to the side now
        yield return new WaitForSeconds(2.5f);

        BlockStickerUI();
        float random = Random.Range(0.0f, 1.0f);

        Side = random > 0.5f;
        IsFlipped = true;

        player.texture = playerTextures[1];
        if (Side) {
            
            flipTexture();
        }

        // give time to react
        yield return new WaitForSeconds(1.0f);

        player.texture = playerTextures[2];
        TimeLabel.text = "Flip them! Don't let them sleep on their belly!";

        while(t > 0.0f && IsFlipped) {
            adjutstTimebar(t, initialtime);
            t -= Time.deltaTime;
            yield return null;
        }

        if(t <= 0.0f) {
            Debug.Log("Task failed!");
            TimeLabel.text = "Oh no!";
            StartGame = false;

            Frame.SetActive(true);
            ButtonLabel.text = "Try again?";
            TitleLabel.text = "Woops! Don't sleep on your belly!";

            yield return null;
        } else {
            Timebar.GetComponent<SpriteRenderer>().color = Color.white;
            TimeLabel.text = "Good job!";
            
            player.texture = playerTextures[1];

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
            resetTimebar(t, initialtime, currWidth, currColor);
            t -= Time.deltaTime;
            yield return null;
        }

        Timebar.transform.localScale = InitialSize;
        yield return new WaitForSeconds(1.0f);

        coroutine = null;
    }
    
    void FlipPerson(bool dir) {
        // reverse sort of logic if they're facing right, we want to flip it lefts
        IsFlipped = false;
        resetOrientation();

        if (!dir) {
           flipTexture(); 
        }
        Debug.Log("Correct!");
    }

    void ResetGame()
    {
        GameTimer = 60.0f;
        TimeLabel.text = "";
        Timebar.transform.localScale = InitialSize;
        Timebar.GetComponent<SpriteRenderer>().color = Color.white;
        player.texture = playerTextures[0];
        IsFlipped = false;
    }

    void GoOnClick()
    {
        Frame.SetActive(false);
        foreach (GameObject SleepingSticker in stickers) {
            SleepingSticker.SetActive(true);
        }

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
                coroutine = random > 0.5f ? TurnEvent() : LooseBandageEvent();
                StartCoroutine(coroutine);
            }

        } else {
            Debug.Log("Game ended, you win!");
            StartGame = false;
            SceneManager.LoadScene(9);
        }
    }

    void Update()
    {
        if (StartGame) {
            GameLoop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDropController : MonoBehaviour
{
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    private GameObject spaces;
    private GameObject stickers;

    // Start is called before the first frame update
    void Start()
    {
        spaces = GameObject.Find("Spaces");
        stickers = GameObject.Find("Stickers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win() {
        //hide stickers & spaces
        for (int i = 0; i < 6; i++) {
            spaces.transform.GetChild(i).gameObject.SetActive(false);
            stickers.transform.GetChild(i).gameObject.SetActive(false);
        }

        foreach (GameObject obj in objectsToHide) {
            obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToShow) {
            obj.SetActive(true);
        }
        SceneManager.LoadScene(13);
    }
}

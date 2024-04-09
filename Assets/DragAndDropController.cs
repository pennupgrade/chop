using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDropController : MonoBehaviour
{
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;
    public GameObject[] imagesToShow;
    public GameObject button;

    private GameObject spaces;
    private GameObject stickers;
    private int currentIndex = 0;

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

    }

    public void DragDropDone() {
        foreach (GameObject obj in objectsToShow) {
            obj.SetActive(false);
        }
        button.SetActive(true);
        imagesToShow[0].SetActive(true);
        
    }

    public void NextObject()
    {
        // Hide the current object
        imagesToShow[currentIndex].SetActive(false);

        // Increment the index to show the next object
        currentIndex = currentIndex + 1;

        if (currentIndex == 4) {
            SceneManager.LoadScene(0);
        } else {
        // Show the next object
            imagesToShow[currentIndex].SetActive(true);
        }
    }
}

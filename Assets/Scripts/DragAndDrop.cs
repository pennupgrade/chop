using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    private GameObject spaces;
    private GameObject stickers;
    private bool isMoving;
    public bool finished;
    public int colour;

    private float startPosX, startPosY; 

    private Vector3 resetPos;

    public GameObject controller;

    void Start()
    {
        spaces = GameObject.Find("Spaces");
        stickers = GameObject.Find("Stickers");
        controller = GameObject.Find("GameController");
        resetPos = this.transform.localPosition;
        finished = false;
    }

    void Update()
    {
        if (isMoving) {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 
                                                                  this.gameObject.transform.localPosition.z);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && !finished) {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isMoving = true;
        }
    }

    private void OnMouseUp() {
        isMoving = false;
        GameObject checkSpace;

        for (int i = 0; i < 8; i++) {
            checkSpace = spaces.transform.GetChild(i).gameObject;
            if (!checkSpace.GetComponent<Spaces>().isFilled) {
                if (Mathf.Abs(this.transform.localPosition.x - checkSpace.transform.localPosition.x) <= 0.5f
                    && Mathf.Abs(this.transform.localPosition.y - checkSpace.transform.localPosition.y) <= 0.5f
                    && colour == checkSpace.GetComponent<Spaces>().colour) {
                    this.transform.localPosition = new Vector3(checkSpace.transform.localPosition.x, checkSpace.transform.localPosition.y, checkSpace.transform.localPosition.z);
                    checkSpace.GetComponent<Spaces>().isFilled = true;
                    finished = true;
                    break;
                }
            } 
        }
        if (!finished) {
            this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        }

        checkWin();
    }

    private void checkWin() {
        bool hasWon1 = true;
        GameObject checkSpace;

        for (int i = 0; i < 6; i++) {
            checkSpace = stickers.transform.GetChild(i).gameObject;
            if (!checkSpace.GetComponent<DragAndDrop>().finished) {
                hasWon1 = false;
            } 
        }

        if (hasWon1) {
            Debug.Log("Part 1 won");
            controller.GetComponent<DragAndDropController>().Win();

            bool hasWon2 = true; 
            for (int i = 6; i < 8; i++) {
                checkSpace = stickers.transform.GetChild(i).gameObject;
                if (!checkSpace.GetComponent<DragAndDrop>().finished) {
                    hasWon2 = false;
                } 
            }

            if (hasWon2) {
                controller.GetComponent<DragAndDropController>().DragDropDone();
            }
        }
    }
}

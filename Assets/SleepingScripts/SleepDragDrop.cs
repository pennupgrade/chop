using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepDragDrop : MonoBehaviour
{
    private GameObject space;
    private GameObject sticker;

    private bool isMoving;
    private bool finished;
    private bool isActive;
    public int colour;

    private float startPosX, startPosY; 

    private Vector3 moveDirection;

    void Start()
    {
        sticker = this.gameObject;
        space = sticker.transform.parent.Find("space").gameObject;
        
        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);

        finished = false;
        isActive = false;
    }

    public Vector3 GetMoveDirection() {
        return moveDirection;
    }

    public int GetColour() {
        return colour;
    }

    public bool IsMoving() {
        return isMoving;
    }

    public bool IsFinished() {
        return finished;
    }

    public bool IsActive() {
        return isActive;
    }

    public void SetActiveState(bool active) {
        isActive = active;
    }

    void Update()
    {
        if (isMoving) {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            sticker.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 
                                                                  sticker.transform.localPosition.z);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && !finished && isActive) {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - sticker.transform.localPosition.x;
            startPosY = mousePos.y - sticker.transform.localPosition.y;

            isMoving = true;
        }
    }

    private void OnMouseUp() {
        isMoving = false;

        float dist = Vector3.Distance(sticker.transform.localPosition, space.transform.localPosition);

        if (dist < 0.5f) {
            isActive = false;
            finished = true;

            moveDirection = Vector3.zero;
            sticker.transform.localPosition = space.transform.localPosition;
            return;
        }

        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
        return;
    }

    public void reset() {
        finished = false;
        isActive = false;
        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
        sticker.transform.localPosition = space.transform.localPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    public GameObject leftSide;
    public GameObject rightSide;

    public GameObject wires;

    GameObject leftHighlight;
    GameObject rightHighlight;

    // Start is called before the first frame update
    void Start()
    {
        leftHighlight = leftSide.transform.GetChild(0).gameObject;
        rightHighlight = rightSide.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leftButtonPressed(int button) {
        leftHighlight.SetActive(true);
        leftHighlight.transform.position = leftSide.transform.GetChild(button).position;
        if (rightHighlight.transform.position == rightSide.transform.GetChild(button).position)
        {
            wires.transform.GetChild(button - 1).gameObject.SetActive(true);
            leftHighlight.SetActive(false);
            rightHighlight.SetActive(false);
        }
    }
    public void rightButtonPressed (int button){
        rightHighlight.SetActive(true);
        rightHighlight.transform.position = rightSide.transform.GetChild(button).position;
        if (leftHighlight.transform.position == leftSide.transform.GetChild(button).position)
        {
            wires.transform.GetChild(button - 1).gameObject.SetActive(true);
            leftHighlight.SetActive(false);
            rightHighlight.SetActive(false);
        }

    }

}


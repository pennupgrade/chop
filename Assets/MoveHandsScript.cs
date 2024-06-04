using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveHandsScript : MonoBehaviour
{
    public float totalTime = 30;//amount of time the player has to survive
    private int clicks = 0;
    float currTime;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject failureMessage;
    public float maxFailureTime = 5;
    float failureTimer;

    Vector3 leftVelocity;
    Vector3 rightVelocity;
    public float moveSpeed = 50;

    Vector3 initLeftPos;
    Vector3 initRightPos;

    float leftTimer;
    float rightTimer;
    public float maxWaitTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        initLeftPos = leftHand.transform.position;
        initRightPos = rightHand.transform.position;
        leftTimer = Random.value * maxWaitTime;
        rightTimer = Random.value * maxWaitTime;
        currTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {   
        // update all timers
        if (leftTimer > 0)
            leftTimer -= Time.deltaTime;
        if (rightTimer > 0)
            rightTimer -= Time.deltaTime;
        if (failureTimer > 0)
            failureTimer -= Time.deltaTime;

        // reduce current time
        currTime -= Time.deltaTime;

        // ??? don't know what this does
        if (leftTimer <= 0)
            leftVelocity = new Vector3(moveSpeed,0,0);
        if (rightTimer <= 0)
            rightVelocity = new Vector3(-moveSpeed,0,0);

        leftHand.transform.position += leftVelocity * Time.deltaTime;
        rightHand.transform.position += rightVelocity * Time.deltaTime;

        // if either hand reaches the failure zone -- player did not click quick enough
        if (leftHand.transform.localPosition.x >= -510 || rightHand.transform.localPosition.x <= 510)
        {
            failureMessage.SetActive(true);
            resetHand(2); // refer to resetHand() to why I set 2,3
            resetHand(3);
            leftVelocity = rightVelocity = Vector3.zero;
            failureTimer = maxFailureTime;
            leftTimer += failureTimer;
            rightTimer += failureTimer;
            currTime = totalTime + failureTimer;
        }
        if (failureTimer <= 0)
            failureMessage.SetActive(false);
        if (currTime <= 0 || clicks >= 4)
            SceneManager.LoadScene(12);
    }

    public void resetHand(int hand)
    {
        // condition that either hand gets clicked s.t it triggers reset
        if (hand <= 1) {
            clicks++;
        } else {
            // hack so that we differentiate between failure reset vs. good reset
            hand -= 2;
        }
        switch (hand)
        {
            case 0:
                leftVelocity = Vector3.zero;
                leftHand.transform.position = initLeftPos;
                leftTimer = Random.value * maxWaitTime;
                break;
            case 1:
                rightVelocity = Vector3.zero;
                rightHand.transform.position = initRightPos;
                rightTimer = Random.value * maxWaitTime;
                break;
            default:
                Debug.LogError("Incorrect hand no");
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaitThenAppear : MonoBehaviour
{
    public Button btn;

    void Start()
    {
        //this.gameObject.SetActive(false);
        StartCoroutine(wait_then_appear());
    }

    private IEnumerator wait_then_appear() {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Coroutine ended");
        btn.gameObject.SetActive(true);
    }
}

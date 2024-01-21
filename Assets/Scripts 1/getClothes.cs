using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class getClothes : MonoBehaviour
{

    public GameObject player;

    private GameObject shirt;

    // Start is called before the first frame update
    void Start()
    {

        //GameObject Player = Car.transform.Find("Player").gameObject;
        shirt = GameObject.Find("ShirtStorage");

        player.GetComponent<RawImage>().texture = shirt.GetComponent<RawImage>().texture;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

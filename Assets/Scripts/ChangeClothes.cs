using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeClothes : MonoBehaviour
{
    public Image image;
    public ArrayList imageList;
    int currImage;
    // Start is called before the first frame update
    void Start()
    {
        imageList = new ArrayList();
        imageList.Add(new Color(1,1,1));
        imageList.Add(new Color(0, 1, 1));
        imageList.Add(new Color(1, 0, 1));
        imageList.Add(new Color(1, 1, 0));
        imageList.Add(new Color(0, 0, 1));
        imageList.Add(new Color(0, 1, 0));
        imageList.Add(new Color(1, 0, 0));

        image.color = (Color)imageList[0];
        currImage = 0;
    }

    public void LeftImage() {
        currImage = currImage-1<0?imageList.Count-1:currImage-1 ;
        image.color = (Color)imageList[currImage];
    }

    public void RightImage()
    {
        currImage = (currImage + 1) % imageList.Count;
        image.color = (Color)imageList[currImage];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

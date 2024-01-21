using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeClothes : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button goButton;

    public RawImage carPreview;
    public RawImage finalShirt;
    public TMPro.TextMeshProUGUI carName;
    public GameObject selectedCar;
    public GameObject[] carList;

    public int index = 0;

    void ChangeDetails()
    {
        selectedCar = carList[index];
        carPreview.texture = selectedCar.GetComponent<Vehicle>().txt;
        carName.text = "Selected shirt: " + selectedCar.GetComponent<Vehicle>().VehicleName;
    }

    // Start is called before the first frame update
    void Start()
    {
        goButton.onClick.AddListener(GoOnClick);
        leftButton.onClick.AddListener(LeftOnClick);
        rightButton.onClick.AddListener(RightOnClick);
        ChangeDetails();
    }

    // Update is called once per frame
    void Update()
    {}

    void GoOnClick()
    {
        finalShirt.texture = selectedCar.GetComponent<Vehicle>().txt;
        DontDestroyOnLoad(finalShirt.gameObject);
    }
        
    void LeftOnClick()
    {
        index = (index - 1);
        if(index < 0){
            index = carList.Length - 1;
        }
        ChangeDetails();
    }

    void RightOnClick()
    {
        index = (index + 1) % carList.Length;
        ChangeDetails();
    }
}

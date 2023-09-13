using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public RawImage carPreview;
    public TMPro.TextMeshProUGUI carName;
    public GameObject selectedCar;
    public GameObject[] carList;
    private int index = 0;

    void ChangeDetails()
    {
        selectedCar = carList[index];
        carPreview.texture = selectedCar.GetComponent<Vehicle>().txt;
        carName.text = "Selected Car: " + selectedCar.GetComponent<Vehicle>().VehicleName;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(LeftOnClick);
        rightButton.onClick.AddListener(RightOnClick);
        ChangeDetails();
    }

    // Update is called once per frame
    void Update()
    {}

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

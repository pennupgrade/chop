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
    public Button goToHospitalButton;

    public RawImage carPreview;
    public TMPro.TextMeshProUGUI carName;
    public GameObject selectedCar;
    public GameObject[] carList;

    private IEnumerator coroutine;
    private int index = 0;

    void ChangeDetails()
    {
        selectedCar = carList[index];
        carPreview.texture = selectedCar.GetComponent<Vehicle>().txt;
        carName.text = "Selected Clothes: " + selectedCar.GetComponent<Vehicle>().VehicleName;
    }

    // Start is called before the first frame update
    void Start()
    {
        goToHospitalButton.gameObject.SetActive(false);

        goButton.onClick.AddListener(GoOnClick);
        leftButton.onClick.AddListener(LeftOnClick);
        rightButton.onClick.AddListener(RightOnClick);
        goToHospitalButton.onClick.AddListener(LeaveCarOnClick);
        ChangeDetails();
    }

    // Update is called once per frame
    void Update()
    {}

    void LeaveCarOnClick() {
        if(coroutine == null) {
        StartCoroutine(coroutine);
        }
        return;
    }

    void GoOnClick()
    {
        if(coroutine == null) {
            StartCoroutine(coroutine);
        }
        return;
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

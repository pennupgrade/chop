using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeCar : MonoBehaviour
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
        carName.text = "Selected Car: " + selectedCar.GetComponent<Vehicle>().VehicleName;
    }

    private IEnumerator LeaveCarSequence() {
        Debug.Log("LeaveCarSequence");

        GameObject AnimatedScene = GameObject.Find("AnimatedScene").gameObject;
        GameObject Player = AnimatedScene.transform.Find("Player").gameObject;

        // set visibility
        Player.SetActive(true);     
        
        // walk to the hospital
        float t = 3.0f;
        float py = Player.transform.position.y;

        while(t > 0.0f) {
            Player.transform.position = new Vector3(Player.transform.position.x + 0.0075f, Player.transform.position.y, Player.transform.position.z);
            
            float dy = Mathf.Sin(t * 40.0f) * 0.075f;
            Player.transform.position = new Vector3(Player.transform.position.x, py + dy, Player.transform.position.z);

            t -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        Debug.Log("switch scenes now");
        SceneManager.LoadScene("InsideChop");


        yield return null;
    }

    private IEnumerator AnimationSequence() {
        Debug.Log("AnimationSequence");

        GameObject UI = GameObject.Find("UI");
        GameObject Frame = UI.transform.Find("Frame").gameObject;
        GameObject AnimatedScene = GameObject.Find("AnimatedScene").gameObject;
        GameObject Car = AnimatedScene.transform.Find("Car").gameObject;
        GameObject Background  = AnimatedScene.transform.Find("Background").gameObject;
        GameObject Road = AnimatedScene.transform.Find("Road").gameObject;
        GameObject Chop = AnimatedScene.transform.Find("Chop").gameObject;
        GameObject Grass = AnimatedScene.transform.Find("RoadGrass").gameObject;
        GameObject BG1 = AnimatedScene.transform.Find("BG1").gameObject;
        GameObject BG2 = AnimatedScene.transform.Find("BG2").gameObject;
        GameObject Bell = AnimatedScene.transform.Find("Bell").gameObject;
        GameObject Statue = AnimatedScene.transform.Find("Statue").gameObject;

        Debug.Log("Frame: " + Frame);
        Car.GetComponent<RawImage>().texture = carPreview.texture;

        float t = 1.25f;

        while(t > 0.0f) {
            Frame.transform.position = new Vector3(Frame.transform.position.x, Frame.transform.position.y + 0.03f, Frame.transform.position.z);
            t -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Start driving: " + selectedCar.GetComponent<Vehicle>().VehicleName);

        float timer = 14.0f;
        float seqLength = 3.0f;
        float outSequence = timer - seqLength;

        float carY = Car.transform.position.y;
        float speed = 0.01f;
        float grassSpeed = 0.05f;
        float speedBG1 = 0.002f;
        float speedBG2 = 0.005f;
        float speedBG3 = 0.007f;

        while(timer > 0.0f) {
            if(timer > outSequence) {
                Background.transform.position = new Vector3(Background.transform.position.x - speed, Background.transform.position.y, Background.transform.position.z);
                speed += Time.deltaTime * 0.01f;
            }
            if(timer > outSequence + 2.0f) {
                Car.transform.position = new Vector3(Car.transform.position.x + speed * 0.7f, Car.transform.position.y, Car.transform.position.z);
            }
            if(timer < seqLength) {
                Background.transform.position = new Vector3(Background.transform.position.x - speed, Background.transform.position.y, Background.transform.position.z);
                speed -= Time.deltaTime * 0.01f;
            }
            if(timer < seqLength - 2.0f) {
                Car.transform.position = new Vector3(Car.transform.position.x - speed * 0.7f, Car.transform.position.y, Car.transform.position.z);
            }

            Road.transform.position = new Vector3(Road.transform.position.x - speed, Road.transform.position.y, Road.transform.position.z);
            Chop.transform.position = new Vector3(Chop.transform.position.x - speed, Chop.transform.position.y, Chop.transform.position.z);
            Grass.transform.position = new Vector3(Grass.transform.position.x - grassSpeed, Grass.transform.position.y, Grass.transform.position.z);
            BG1.transform.position = new Vector3(BG1.transform.position.x - speedBG1, BG1.transform.position.y, BG1.transform.position.z);
            BG2.transform.position = new Vector3(BG2.transform.position.x - speedBG2, BG2.transform.position.y, BG2.transform.position.z);
            Bell.transform.position = new Vector3(Bell.transform.position.x - speedBG3, Bell.transform.position.y, Bell.transform.position.z);
            Statue.transform.position = new Vector3(Statue.transform.position.x - speedBG3, Statue.transform.position.y, Statue.transform.position.z);

            float dy = Mathf.Sin(timer * 40.0f) * 0.075f;
            Car.transform.position = new Vector3(Car.transform.position.x, carY + dy, Car.transform.position.z);

            timer -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Done driving");

        yield return new WaitForSeconds(2.5f);

        // go make a button that goes to the next scene / it basically says "enter hospital"
        goToHospitalButton.gameObject.SetActive(true);
        coroutine = null;
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
            coroutine = LeaveCarSequence();
            StartCoroutine(coroutine);
        }
        return;
    }

    void GoOnClick()
    {
        if(coroutine == null) {
            coroutine = AnimationSequence();
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

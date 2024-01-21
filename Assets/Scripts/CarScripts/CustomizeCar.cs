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

    public GameObject UI;
    GameObject Frame;
    GameObject AnimatedScene;
    GameObject Car; 
    GameObject Background;
    GameObject Road;
    GameObject Chop;
    GameObject Grass;
    GameObject BG1;
    GameObject BG2;
    GameObject Bell;
    GameObject Statue;

    void ChangeDetails()
    {
        selectedCar = carList[index];
        carPreview.texture = selectedCar.GetComponent<Vehicle>().txt;
        carName.text = "Selected Car: " + selectedCar.GetComponent<Vehicle>().VehicleName;
    }

    private IEnumerator LeaveCarSequence() {
        Debug.Log("LeaveCarSequence");

        GameObject Player = Car.transform.Find("Player").gameObject;

        // set visibility
        Player.SetActive(true);     
        
        // walk to the hospital
        float t = 3.0f;
        float py = Player.transform.position.y;

        while(t > 0.0f) {
            float dy = Mathf.Sin(t * 40.0f) * 0.875f;
            Player.transform.Translate(1.4f * Time.deltaTime, dy * Time.deltaTime, 0.0f);

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
        Debug.Log("Frame: " + Frame);
        Car.GetComponent<RawImage>().texture = carPreview.texture;
        
        float t = 3.00f;
        while(t > 0.0f) {
            Frame.transform.Translate(0.0f, 3.0f * Time.deltaTime, 0.0f);
            t -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Start driving: " + selectedCar.GetComponent<Vehicle>().VehicleName);

        float timer = 14.0f;
        float seqLength = 3.0f;
        float outSequence = timer - seqLength;

        float carY = Car.transform.position.y;
        float speed = 1.30f;
        float grassSpeed = 8.55f;

        while(timer > 0.0f) {
            float translateSpeed = speed;

            if(timer > outSequence) {
                Background.transform.Translate(-2.0f * translateSpeed * Time.deltaTime, 0.0f, 0.0f);
                speed += 0.1f * Time.deltaTime;
            }
            if(timer > outSequence - 1.0f) {
                Car.transform.Translate(translateSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
            if(timer < seqLength) {
                speed -= 0.1f * Time.deltaTime;
            }
            if(timer < seqLength - 4.0f) {
                translateSpeed *= 0.7f;
                Car.transform.Translate(-translateSpeed * Time.deltaTime, 0.0f, 0.0f);
                Chop.transform.Translate(translateSpeed * Time.deltaTime, 0.0f, 0.0f);
            }

            float gSpeedDelta = grassSpeed + speed;
            
            float speedBG1 = gSpeedDelta * 0.1f;
            float speedBG2 = gSpeedDelta * 0.2f;
            float speedBG3 = gSpeedDelta * 0.5f;

            Road.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
            Grass.transform.Translate(-gSpeedDelta * Time.deltaTime, 0.0f, 0.0f);
            Chop.transform.Translate(-gSpeedDelta * Time.deltaTime, 0.0f, 0.0f);
            BG1.transform.Translate(-speedBG1 * Time.deltaTime, 0.0f, 0.0f);
            BG2.transform.Translate(-speedBG2 * Time.deltaTime, 0.0f, 0.0f);
            Bell.transform.Translate(speedBG3 * Time.deltaTime, 0.0f, 0.0f);
            Statue.transform.Translate(speedBG3 * Time.deltaTime, 0.0f, 0.0f);

            float dy = Mathf.Sin(timer * 40.0f) * 0.875f;
            Car.transform.Translate(0.0f, dy * Time.deltaTime, 0.0f);

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
        
        Frame = UI.transform.Find("Frame").gameObject;
        AnimatedScene = GameObject.Find("AnimatedScene").gameObject;
        Car = AnimatedScene.transform.Find("Car").gameObject;
        Background  = AnimatedScene.transform.Find("Background").gameObject;
        Road = AnimatedScene.transform.Find("Road").gameObject;
        BG1 = AnimatedScene.transform.Find("BG1").gameObject;
        BG2 = AnimatedScene.transform.Find("BG2").gameObject;
        Chop = AnimatedScene.transform.Find("Chop").gameObject;
        Grass = AnimatedScene.transform.Find("RoadGrass").gameObject;
        Bell = Grass.transform.Find("Bell").gameObject;
        Statue = Grass.transform.Find("Statue").gameObject;
    }

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

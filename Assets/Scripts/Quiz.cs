using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    Queue questions = new Queue();

    Question currQuestion;

    Button[] Buttons;

    bool corrAns = false;

    public TMP_Text questionText;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button option4Button;
    public Button nextQuestionButton;

    [SerializeField] Sprite wrongButtonSprite;
    [SerializeField] Sprite rightButtonSprite;
    [SerializeField] Sprite greyButtonSprite;

    // Start is called before the first frame update
    void Start()
    {
        Buttons = new Button[] { option1Button, option2Button, option3Button, option4Button };

        questions.Enqueue(new Question("What will Ahtish ask you about in the call?", new string[]{ "Your dinner", "Your sleep", "School", "Swimming" }, 2));
        questions.Enqueue(new Question("What is the purpose of this study?", new string[] { "Eat ice cream", "Get a dog", "To help teens and adults", "To dance" }, 3));
        questions.Enqueue(new Question("What is a risk?", new string[] { "Stickers bother you", "Nose prongs bother you", "You feel itchy", "All of the above" }, 4));
        questions.Enqueue(new Question("What side should you not sleep on?", new string[] { "Left", "Right", "Belly", "Up" }, 2));
        questions.Enqueue(new Question("What can you do to distract yourself when you're uncomfortable?", new string[] { "Play videogames", "Read a book", "Listen to music", "All of the above" }, 4));
        questions.Enqueue(new Question("Where can you apply lotion or hair products?", new string[] { "Face", "Chin", "Chest", "None of the above, don't!" }, 4));

        setQuestion((Question) questions.Dequeue());
    }

    void setQuestion(Question q)
    {
        currQuestion = q;
        questionText.text = q.GetQuestion();
        option1Button.GetComponentInChildren<TMP_Text>().text = q.GetOptions()[0];
        option2Button.GetComponentInChildren<TMP_Text>().text = q.GetOptions()[1];
        option3Button.GetComponentInChildren<TMP_Text>().text = q.GetOptions()[2];
        option4Button.GetComponentInChildren<TMP_Text>().text = q.GetOptions()[3];
        nextQuestionButton.interactable = false;
    }

    public void OptionClick(int button) {
        if (Equals(currQuestion,null) || corrAns)
            return;
        if (button == currQuestion.GetCorrect())
        {
            corrAns = true;
            nextQuestionButton.interactable = true;
            Buttons[button - 1].GetComponent<Image>().sprite = rightButtonSprite;
        }
        else
            Buttons[button - 1].GetComponent<Image>().sprite = wrongButtonSprite;
    }

    public void advanceQueue() {
        if (questions.Count != 0)
        {
            foreach (Button b in Buttons)
            {
                b.GetComponent<Image>().sprite = greyButtonSprite;
            }
            corrAns = false;
            setQuestion((Question)questions.Dequeue());
        }
        else
        {
            SceneManager.LoadScene(11);
        }
    }
}

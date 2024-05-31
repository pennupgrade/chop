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

        questions.Enqueue(new Question("What will The Researchers ask you about in the call?", new string[]{ "Your dinner", "Your sleep", "School", "Swimming" }, 2));
        questions.Enqueue(new Question("Who are you helping by doing research?", new string[] { "Teachers", "Dogs", "People with Down Syndrome", "Babies" }, 3));
        questions.Enqueue(new Question("Which of these things might happen to you?", new string[] { "Stickers bother you", "Nose prongs bother you", "You feel itchy", "The bandage is uncomfortable"}, 5));
        questions.Enqueue(new Question("What side should you <b>not</b> sleep on?", new string[] { "Left side", "Right side", "Belly", "Back" }, 3));
        questions.Enqueue(new Question("What are some things you can do while you're waiting to go to bed?", new string[] { "Play videogames", "Read a book", "Listen to music", "Any sitting activity" }, 5));

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
        if (Equals(currQuestion, null) || corrAns)
            return;
        if (button == currQuestion.GetCorrect())
        {
            corrAns = true;
            nextQuestionButton.interactable = true;
            Buttons[button - 1].GetComponent<Image>().sprite = rightButtonSprite;
        }
        else if (currQuestion.GetOptions().Length < currQuestion.GetCorrect())
        {
            Buttons[button - 1].GetComponent<Image>().sprite = rightButtonSprite;
            nextQuestionButton.interactable = true;
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

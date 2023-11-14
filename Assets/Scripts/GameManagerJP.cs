using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerJP : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private int correct = 0;
    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    public AudioClip correctSound;
    public AudioClip incorrectSound;



    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        Debug.Log(currentQuestion.fact + "is" + currentQuestion.isTrue);
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "SO TRUE BESTIE";
            falseAnswerText.text = "MMM YEA NO";
        }
        else
        {
            trueAnswerText.text = "ITS OKAY NO ONE SAW";
            falseAnswerText.text = "YES BESTIE";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        animator.SetTrigger("Reset");
        SetCurrentQuestion();
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            correct++;
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(correctSound);
            Debug.Log("CORRECT");
        }
        else
        {
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(incorrectSound);
            Debug.Log("WRONG");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            correct++;
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(correctSound);
            Debug.Log(correct);
        }
        else
        {
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(incorrectSound);
            Debug.Log(correct);
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void Update()
    {
        if(correct >= 10)
        {
            GameManager.instance.SetMinigameComplete("Jeopardy");
            SceneManager.LoadScene("Map");
        }
    }
}

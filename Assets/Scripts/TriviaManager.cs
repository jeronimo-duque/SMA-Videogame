using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriviaGameOverEvent : UnityEvent<string, string,string> { }
// First param -> Title feedback message
// Second param -> Feedback message
// Third param -> Suggestion feedback message
public class TriviaManager : MonoBehaviour
{

    [Header("Navigation between questions")]
    [SerializeField] private List<GameObject> questions;
    [SerializeField] private int currentQuestion = 0;


    [Space(10)]
    [Header("Feedback UI")]
    [SerializeField] private GameObject triviaUI;
    [SerializeField] private GameObject positiveFeedbackUI;
    [SerializeField] private TextMeshProUGUI positiveFeedbackTitle;
    [SerializeField] private TextMeshProUGUI positiveFeedbackMessage;
    [SerializeField] private TextMeshProUGUI positiveSuggestionMessage;

    [Header("Negative feedback")]
    [SerializeField] private GameObject negativeFeedbackUI;
    [SerializeField] private TextMeshProUGUI negativeFeedbackTitle;
    [SerializeField] private TextMeshProUGUI negativeFeedbackMessage;
    [SerializeField] private TextMeshProUGUI negativeSuggestionMessage;

    [Header("Win feedback")]
    [SerializeField] private GameObject winFeedbackUI;


    public void ShowNegativeFeedback(string title, string message, string suggestion)
    {
        Debug.Log("Bad");
        // Put the information in each TMPro assigned.
        negativeFeedbackUI.SetActive(true);
        negativeFeedbackTitle.text = title;
        negativeFeedbackMessage.text = message;
        negativeSuggestionMessage.text = suggestion;

        // Hide the question interface
        questions[currentQuestion].SetActive(false);
    }

    public void ShowPositiveFeedback(string title, string message, string suggestion)
    {
        Debug.Log("Nice");
        // Put the information in each TMPro assigned.
        positiveFeedbackUI.SetActive(true);
        positiveFeedbackTitle.text = title;
        positiveFeedbackMessage.text = message;
        positiveSuggestionMessage.text = suggestion;

        // Hide the question interface
        questions[currentQuestion].SetActive(false);

    }

    public void RetryQuestion()
    {
        negativeFeedbackUI.SetActive(false);

        // Show again the current question.
        questions[currentQuestion].SetActive(true);
    }

    public void ShowNextQuestion()
    {

        // If the index hasn't exceeded the maximum list length, execute the logic for the next question.
        if (currentQuestion < questions.Count)
        {
            // Hide the current question
            questions[currentQuestion].SetActive(false);
            currentQuestion++;
  
            // If the user has answered all the questions, don't show anymore.
            if(currentQuestion == questions.Count)
            {
                Debug.Log("GG");
                winFeedbackUI.SetActive(true);
                ScoreManager.Instance.AddStar(); //Progress Manager.
                return;
            }
            
            // Active the next question and hide the positive feedback.
            questions[currentQuestion].SetActive(true);
            positiveFeedbackUI.SetActive(false);
        }
    }

    public void StartTrivia()
    {
        triviaUI.gameObject.SetActive(true);
    }

    public void HideTrivia()
    {
        triviaUI.gameObject.SetActive(false);
    }
}

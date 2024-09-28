using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Possible options")]
    [SerializeField] private List<TextMeshProUGUI> answerOptions;

    [Header("Correct option")]
    [SerializeField] private TextMeshProUGUI correctAnswer;

    [Space(10)]
    [Header("Positive feedback")]
    public TriviaGameOverEvent answerCorrectlyEvent;
    [SerializeField] private string positiveFeedbackTitle;
    [SerializeField] private string positiveFeedbackMessage;
    [SerializeField] private string positiveSuggestionTitle;

    [Header("Negative feedback")]
    public TriviaGameOverEvent gameOverEvent;
    [SerializeField] private string negativeFeedbackTitle;
    [SerializeField] private string negativeFeedbackMessage;
    [SerializeField] private string negativeSuggestionTitle;

    public void ValidateAnswer(TextMeshProUGUI selectedOption)
    {
        
        if (selectedOption.text != correctAnswer.text)
        {
            // Invoke the game over event with 3 args.
            gameOverEvent.Invoke(negativeFeedbackTitle, negativeFeedbackMessage, negativeSuggestionTitle);
            return;
        }

        answerCorrectlyEvent.Invoke(positiveFeedbackTitle, positiveFeedbackMessage,positiveSuggestionTitle);


    }

    public void SetCorrectAnswer(TextMeshProUGUI correctAnswerText)
    {
        correctAnswer = correctAnswerText;
    }
}

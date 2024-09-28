using NUnit.Framework;
using UnityEngine;
using TMPro;

public class TriviaManagerTest
{

    private QuestionManager questionManager;
    private TextMeshProUGUI correctAnswerText;
    private TextMeshProUGUI selectedAnswerText;

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject that will contain the QuestionManager
        GameObject go = new GameObject();
        questionManager = go.AddComponent<QuestionManager>();

        // Configure the TextMeshProUGUI for the responses
        correctAnswerText = new GameObject().AddComponent<TextMeshProUGUI>();
        selectedAnswerText = new GameObject().AddComponent<TextMeshProUGUI>();

        // Assign the correct text to the QuestionManager
        questionManager.SetCorrectAnswer(correctAnswerText);

        // Create instances of events and assign them to the QuestionManager
        questionManager.answerCorrectlyEvent = new TriviaGameOverEvent();
        questionManager.gameOverEvent = new TriviaGameOverEvent();
    }

    [Test]
    public void ValidateAnswer_CorrectAnswer_InvokesCorrectAnswerEvent()
    {
        // * ARANGE
        // --------------------

        // Set the correct answer.
        correctAnswerText.text = "Correct Answer";

        // Set the selected answer to be the correct one.
        selectedAnswerText.text = "Correct Answer";

        // Use a mock or a variable to verify if the event was called.
        bool eventCalled = false;
        questionManager.answerCorrectlyEvent.AddListener((title, message, suggestion) => eventCalled = true);

        // * ACT
        // --------------------
        questionManager.ValidateAnswer(selectedAnswerText);

        // * ASSERT
        // --------------------
        Assert.IsTrue(eventCalled, "El evento de respuesta correcta no fue invocado.");
    }

    [Test]
    public void ValidateAnswer_IncorrectAnswer_InvokesGameOverEvent()
    {
        // * ARANGE
        // --------------------

        // Set the correct answer.
        correctAnswerText.text = "Correct Answer";

        // Configurar la respuesta seleccionada que es incorrecta
        selectedAnswerText.text = "Incorrect Answer";

        // Set the selected answer to be the correct one.
        bool eventCalled = false;
        questionManager.gameOverEvent.AddListener((title, message, suggestion) => eventCalled = true);

        // * ACT
        // --------------------
        questionManager.ValidateAnswer(selectedAnswerText);

        // * ASSERT
        // --------------------
        Assert.IsTrue(eventCalled, "El evento de game over no fue invocado.");
    }

}

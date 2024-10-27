using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrainstormingMinigameController : MonoBehaviour
{
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private int _scoreToWin = 10;
    [SerializeField] private bool _isMinigameActive = false;

    [Header("Spawn controller")]
    [SerializeField] private BoxCollider2D _spawnZone;
    [SerializeField] private List<GameObject> _bulbsListToInvoke;
    [SerializeField] private float _spawnInterval = 2f;
    private Coroutine _spawnCoroutine;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    public GameObject nextDialogueTrigger;

    void Update()
    {
        if (_isMinigameActive && _currentScore >= _scoreToWin)
        {
            ResetMinigame();
        }
    }

    IEnumerator GeneateRandomBulbs()
    {
        _isMinigameActive = true;
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            Vector2 randomPosition = GetRandomBulbSpawn();
            int randomIndex = UnityEngine.Random.Range(0, _bulbsListToInvoke.Count);
            GameObject bulbToInvoke = _bulbsListToInvoke[randomIndex];
            Instantiate(bulbToInvoke, randomPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomBulbSpawn()
    {
        Bounds spawnLimits = _spawnZone.bounds;
        float x = UnityEngine.Random.Range(spawnLimits.min.x, spawnLimits.max.x);
        float y = UnityEngine.Random.Range(spawnLimits.min.y, spawnLimits.max.y);
        return new Vector2(x, y);
    }

    public void UpdateScoreText(int currentScore)
    {
        _currentScoreText.text = currentScore.ToString("00");
    }

    public void AddScore(int score)
    {
        _currentScore += score;

        if (_currentScore < 0)
        {
            _currentScore = 0;
        }

        UpdateScoreText(_currentScore);
    }

    public void StartMinigameOnce()
    {
        // Inicia el minijuego solo si no está activo
        if (!_isMinigameActive)
        {
            _spawnCoroutine = StartCoroutine(GeneateRandomBulbs());
        }
    }

    private void ResetMinigame()
    {
        StopCoroutine(_spawnCoroutine);
        GameObject[] bulbs = GameObject.FindGameObjectsWithTag("Bulb");

        foreach (GameObject objeto in bulbs)
        {
            Destroy(objeto);
        }

        _isMinigameActive = false;
        _currentScore = 0;

        if (nextDialogueTrigger != null)
        {
            nextDialogueTrigger.SetActive(true);
        }

        UnityEngine.Debug.Log("GANASTE");
    }
}

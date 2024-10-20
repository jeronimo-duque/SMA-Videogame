using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrainstormingMinigameController : MonoBehaviour
{
    

    [Space(8)]
    [Header("Game logic")]
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private int _scoreToWin = 10;
    [SerializeField] private bool _isMinigameActive = false;

    [Header("Spawn controller")]
    [SerializeField] private BoxCollider2D _spawnZone;
    [SerializeField] private List<GameObject> _bulbsListToInvoke;
    [SerializeField] private float _spawnInterval = 2f;
    private Coroutine _spawnCoroutine;

    [Space(8)]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        _spawnCoroutine = StartCoroutine(GeneateRandomBulbs());
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMinigameActive == true && _currentScore >= _scoreToWin)
        {
            ResetMinigame();
        }
    }

 

    IEnumerator GeneateRandomBulbs()
    {
        _isMinigameActive = true;
        while (true)
        {
            // 1. Wait time interval
            yield return new WaitForSeconds(_spawnInterval);

            // 2. Instantiate a new random bulb.
            Vector2 randomPosition = GetRandomBulbSpawn();


            // 3. Get a random bulb.
            int randomIndex = UnityEngine.Random.Range(0, _bulbsListToInvoke.Count);
            GameObject bulbToInvoke = _bulbsListToInvoke[randomIndex];

            Instantiate(bulbToInvoke, randomPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomBulbSpawn()
    {
        Bounds spawnLimits = _spawnZone.bounds;

        
        // Generates a random position within the collider's boundary.
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

    public void StartMinigame()
    {
        _spawnCoroutine = StartCoroutine(GeneateRandomBulbs());
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
        Debug.Log("GANASTE");
    }
}

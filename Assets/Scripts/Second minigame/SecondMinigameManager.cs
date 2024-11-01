using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondMinigameManager : MonoBehaviour, IMinigameController
{
    [Header("Minigame logic")]
    [SerializeField] private PolygonCollider2D _spawnArea;
    [SerializeField] private List<GameObject> _notesGOList;
    private int _currentPickUpNotes = 0;
    private List<Vector2> _randomSpawnPointsList = new List<Vector2>();
    private int _totalNotes = 8;

    [Space(8)]
    [Header("Countdown logic")]
    [SerializeField] private float _timeToCompleteMinigame = 15f;
    private float _countdownRemainingTime;
    private Coroutine _countDownCoroutine;
    private bool _isMinigameComplete = false;
    private bool _isMinigameActive = false;

    [Space(8)]
    [Header("UI")]
    [SerializeField] private GameObject _countdownInterface;
    [SerializeField] private TextMeshProUGUI _countDownText;

    public GameObject nextDialogueTrigger;

    void Start()
    {
        GenerateRandomSpawnPoint();
    }

    [ContextMenu("Start minigame")]
    public void StartMinigame()
    {
        if (!_isMinigameActive)
        {
            _isMinigameActive = true;
            _countdownInterface.SetActive(true);
            _countdownRemainingTime = _timeToCompleteMinigame;
            _countDownCoroutine = StartCoroutine(StartCountDown());
            SpawnNote();
        }
    }

    public void StartMinigameOnce()
    {
        StartMinigame();
    }

    public void PickUpNote()
    {
        _currentPickUpNotes++;

        if (_currentPickUpNotes == _totalNotes)
        {
            WinMinigame();
        }
        else
        {
            SpawnNote();
        }
    }

    public void WinMinigame()
    {
        StopCoroutine(_countDownCoroutine);
        _isMinigameComplete = true;
        _countdownInterface.SetActive(false);
        UnityEngine.Debug.Log("GANASTE, SOS MUY MAKIA");
        ScoreManager.Instance.AddStar(); //Progress Manager.
        nextDialogueTrigger.SetActive(true);
    }

    private void GenerateRandomSpawnPoint()
    {
        Bounds limitsSpawnArea = _spawnArea.bounds;
        Vector2 randomSpawnPoint;
        Vector2 lastRandomSpawnPoint = Vector2.zero;

        while (_randomSpawnPointsList.Count < 8)
        {
            float x = UnityEngine.Random.Range(limitsSpawnArea.min.x, limitsSpawnArea.max.x);
            float y = UnityEngine.Random.Range(limitsSpawnArea.min.y, limitsSpawnArea.max.y);
            randomSpawnPoint = new Vector2(x, y);

            if (_spawnArea.OverlapPoint(randomSpawnPoint))
            {
                if (_randomSpawnPointsList.Count == 0 || Vector2.Distance(lastRandomSpawnPoint, randomSpawnPoint) >= 1.5f)
                {
                    _randomSpawnPointsList.Add(randomSpawnPoint);
                    lastRandomSpawnPoint = randomSpawnPoint;
                }
            }
        }
    }

    public void SpawnNote()
    {
        if (_currentPickUpNotes < 8)
        {
            Instantiate(_notesGOList[_currentPickUpNotes], _randomSpawnPointsList[_currentPickUpNotes], Quaternion.identity);
        }
    }

    private void GameOver()
    {
        _countdownInterface.SetActive(false);
        UnityEngine.Debug.Log("PERDIÓ MI REY");
    }

    private IEnumerator StartCountDown()
    {
        while (_countdownRemainingTime > 0)
        {
            int minutes = (int)(_countdownRemainingTime / 60);
            int seconds = (int)_countdownRemainingTime % 60;

            _countDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1);
            _countdownRemainingTime--;
        }

        if (_isMinigameComplete == false)
        {
            GameOver();
        }
    }
}

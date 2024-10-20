using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbManager : MonoBehaviour
{
    [Header("Bulb information")]
    [SerializeField] private int _scoreToGive = 1;
    private BrainstormingMinigameController _minigameController;

    // Start is called before the first frame update
    void Start()
    {
         _minigameController = GameObject.Find("BrainstormingManager").GetComponent<BrainstormingMinigameController>();
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _minigameController.AddScore(_scoreToGive);

            Destroy(gameObject);

        }
    }
}

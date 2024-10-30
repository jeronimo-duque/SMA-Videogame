using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollectable : MonoBehaviour
{
    private SecondMinigameManager _secondMinigameManager;
    // Start is called before the first frame update
    void Start()
    {
        _secondMinigameManager = GameObject.Find("Minigame controller").GetComponent<SecondMinigameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _secondMinigameManager.PickUpNote();
            Destroy(gameObject);
        }
    }
}
